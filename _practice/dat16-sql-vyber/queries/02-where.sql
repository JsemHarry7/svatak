SELECT * FROM Student WHERE vek BETWEEN 16 AND 18;
SELECT jmeno, prijmeni, trida_id FROM Student WHERE trida_id IN (1, 3, 5);
SELECT * FROM Student WHERE prijmeni LIKE 'n%';
SELECT * FROM Student WHERE prijmeni LIKE '%ov%';
SELECT * FROM Student WHERE email IS NULL;
SELECT jmeno, prijmeni, trida_id FROM Student WHERE vek > 17 AND trida_id IN (3, 4);