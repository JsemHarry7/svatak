SELECT jmeno,prijmeni FROM Student;
SELECT jmeno,prijmeni,vek FROM Student WHERE vek > 17;
SELECT jmeno,prijmeni,vek FROM Student ORDER BY vek asc LIMIT 5;
SELECT DISTINCT rocnik FROM Trida;
SELECT nazev, kredity FROM Predmet WHERE kredity >= 5 ORDER BY kredity DESC;