import sqlite3, os, random
from datetime import date, timedelta

if os.path.exists('exams.sqlite'):
    os.remove('exams.sqlite')

conn = sqlite3.connect('exams.sqlite')
c = conn.cursor()

# Schema
c.executescript('''
CREATE TABLE Trida (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    nazev TEXT NOT NULL UNIQUE,
    rocnik INTEGER NOT NULL CHECK (rocnik BETWEEN 1 AND 4)
);

CREATE TABLE Predmet (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    nazev TEXT NOT NULL UNIQUE,
    kredity INTEGER NOT NULL CHECK (kredity > 0)
);

CREATE TABLE Ucitel (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    jmeno TEXT NOT NULL,
    prijmeni TEXT NOT NULL,
    aprobace TEXT,
    email TEXT UNIQUE
);

CREATE TABLE Student (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    jmeno TEXT NOT NULL,
    prijmeni TEXT NOT NULL,
    vek INTEGER CHECK (vek BETWEEN 14 AND 25),
    email TEXT UNIQUE,
    trida_id INTEGER,
    FOREIGN KEY (trida_id) REFERENCES Trida(id) ON DELETE SET NULL
);

CREATE TABLE Hodnoceni (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    student_id INTEGER NOT NULL,
    predmet_id INTEGER NOT NULL,
    ucitel_id INTEGER,
    skore INTEGER NOT NULL CHECK (skore BETWEEN 0 AND 100),
    datum TEXT NOT NULL,
    FOREIGN KEY (student_id) REFERENCES Student(id) ON DELETE CASCADE,
    FOREIGN KEY (predmet_id) REFERENCES Predmet(id) ON DELETE CASCADE,
    FOREIGN KEY (ucitel_id) REFERENCES Ucitel(id) ON DELETE SET NULL
);
''')

# Data
tridy = [('1.A', 1), ('1.B', 1), ('2.A', 2), ('3.A', 3), ('4.A', 4)]
c.executemany('INSERT INTO Trida (nazev, rocnik) VALUES (?, ?)', tridy)

predmety = [
    ('Matematika', 5),
    ('Programovani v C#', 6),
    ('Databaze', 4),
    ('Web Development', 4),
    ('Cesky jazyk', 3),
    ('Anglictina', 3),
]
c.executemany('INSERT INTO Predmet (nazev, kredity) VALUES (?, ?)', predmety)

ucitele = [
    ('Jan', 'Novak', 'Matematika, Fyzika', 'novak@skola.cz'),
    ('Petra', 'Svobodova', 'Programovani, Databaze', 'svobodova@skola.cz'),
    ('Tomas', 'Dvorak', 'Web Development', 'dvorak@skola.cz'),
    ('Hana', 'Prochazkova', 'Cesky jazyk', 'prochazkova@skola.cz'),
    ('Karel', 'Cerny', 'Anglictina', 'cerny@skola.cz'),
]
c.executemany('INSERT INTO Ucitel (jmeno, prijmeni, aprobace, email) VALUES (?, ?, ?, ?)', ucitele)

studenti = [
    ('Anna', 'Novakova', 16, 'novakova.anna@student.cz', 1),
    ('Petr', 'Svoboda', 16, 'svoboda.petr@student.cz', 1),
    ('Marie', 'Dvorakova', 17, 'dvorakova.marie@student.cz', 1),
    ('Jakub', 'Cerny', 16, 'cerny.jakub@student.cz', 2),
    ('Tereza', 'Kralova', 15, 'kralova.tereza@student.cz', 2),
    ('David', 'Pokorny', 16, 'pokorny.david@student.cz', 2),
    ('Lucie', 'Vesela', 17, 'vesela.lucie@student.cz', 3),
    ('Filip', 'Vlk', 18, 'vlk.filip@student.cz', 3),
    ('Eliska', 'Liskova', 17, 'liskova.eliska@student.cz', 3),
    ('Tomas', 'Ryba', 18, 'ryba.tomas@student.cz', 4),
    ('Karolina', 'Mala', 18, 'mala.karolina@student.cz', 4),
    ('Jan', 'Velky', 19, 'velky.jan@student.cz', 4),
    ('Adam', 'Bily', 19, None, 5),  # student bez emailu (pro IS NULL test)
    ('Klara', 'Cerna', 19, 'cerna.klara@student.cz', 5),
    ('Martin', 'Modry', 20, 'modry.martin@student.cz', 5),
    ('Veronika', 'Zelena', 18, 'zelena.veronika@student.cz', 4),
    ('Pavel', 'Zluty', 17, 'zluty.pavel@student.cz', 3),
    ('Barbora', 'Ruzova', 16, 'ruzova.barbora@student.cz', None),  # student bez třídy
]
c.executemany('INSERT INTO Student (jmeno, prijmeni, vek, email, trida_id) VALUES (?, ?, ?, ?, ?)',
              studenti)

# Hodnoceni - random ale realistic
random.seed(42)
c.execute('SELECT id FROM Student')
student_ids = [r[0] for r in c.fetchall()]
c.execute('SELECT id FROM Predmet')
predmet_ids = [r[0] for r in c.fetchall()]
c.execute('SELECT id FROM Ucitel')
ucitel_ids = [r[0] for r in c.fetchall()]

# Mapping predmet → ucitel (kdo daný předmět učí)
predmet_ucitel = {1: 1, 2: 2, 3: 2, 4: 3, 5: 4, 6: 5}  # Matematika→Novak atd.

hodnoceni = []
base_date = date(2026, 1, 15)
for sid in student_ids:
    # Každý student má hodnocení z 3-6 předmětů
    num_predmety = random.randint(3, 6)
    chosen_predmety = random.sample(predmet_ids, num_predmety)
    for pid in chosen_predmety:
        # 1-3 hodnocení z každého předmětu
        num_hodnoceni = random.randint(1, 3)
        for _ in range(num_hodnoceni):
            skore = random.randint(45, 100)
            datum = (base_date + timedelta(days=random.randint(0, 120))).isoformat()
            uid = predmet_ucitel.get(pid)
            hodnoceni.append((sid, pid, uid, skore, datum))

# Adam Bily ma vsechna hodnoceni bez ucitele (pro IS NULL test)
adam_id = 13
c.execute('DELETE FROM Hodnoceni WHERE student_id = ?', (adam_id,))
hodnoceni = [h for h in hodnoceni if h[0] != adam_id]

# Pridat 1 studenta bez hodnoceni (pro anti-join pattern)
# Barbora (id=18) nebude mit zadne hodnoceni
hodnoceni = [h for h in hodnoceni if h[0] != 18]

c.executemany('INSERT INTO Hodnoceni (student_id, predmet_id, ucitel_id, skore, datum) VALUES (?, ?, ?, ?, ?)',
              hodnoceni)

conn.commit()

# Sanity check
print('=== Counts ===')
for table in ['Trida', 'Predmet', 'Ucitel', 'Student', 'Hodnoceni']:
    c.execute(f'SELECT COUNT(*) FROM {table}')
    print(f'{table}: {c.fetchone()[0]}')

conn.close()
print('\nexams.sqlite created!')
