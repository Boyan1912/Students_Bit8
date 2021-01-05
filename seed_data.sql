USE mydb;

INSERT INTO student (first_name, last_name, date_of_birth)
VALUES('Ivan', 'Ivanov', STR_TO_DATE('17/1/1995', '%d/%m/%Y')),
('Georgi', 'Georgiev', STR_TO_DATE('18/12/1996', '%d/%m/%Y')),
('Hristo', 'Dimitrov', STR_TO_DATE('22/4/2000', '%d/%m/%Y')),
('Kalpazan', 'Petrov', STR_TO_DATE('17/11/2001', '%d/%m/%Y')),
('Dimiter', 'Mitev', STR_TO_DATE('17/10/1995', '%d/%m/%Y')),
('Martin', 'Ivanov', STR_TO_DATE('17/9/1996', '%d/%m/%Y')),
('Peter', 'Hristov', STR_TO_DATE('1/12/1997', '%d/%m/%Y')),
('Stoyko', 'Ivanov', STR_TO_DATE('2/12/1998', '%d/%m/%Y')),
('Hristo', 'Valev', STR_TO_DATE('12/12/1992', '%d/%m/%Y')),
('Rumen', 'Ivanov', STR_TO_DATE('17/2/1995', '%d/%m/%Y'));

INSERT INTO semester (name, start_date, end_date) 
VALUES ('Semester 1', STR_TO_DATE('11/9/2019', '%d/%m/%Y'), STR_TO_DATE('3/3/2020', '%d/%m/%Y')),
('Semester 2', STR_TO_DATE('11/9/2019', '%d/%m/%Y'), STR_TO_DATE('3/3/2020', '%d/%m/%Y')),
('Semester 3', STR_TO_DATE('11/9/2019', '%d/%m/%Y'), STR_TO_DATE('3/3/2020', '%d/%m/%Y')),
('Semester 4', STR_TO_DATE('11/9/2019', '%d/%m/%Y'), STR_TO_DATE('3/3/2020', '%d/%m/%Y')),
('Semester 5', STR_TO_DATE('11/9/2019', '%d/%m/%Y'), STR_TO_DATE('3/3/2020', '%d/%m/%Y'));

INSERT INTO mydb.discipline (name, professor_name, score, id_semester)
VALUES ('sql', 'John Johnson', 3.5, (SELECT id_semester FROM semester WHERE name = 'Semester 1')),
('jquery', 'Peter Petrov', 5, (SELECT id_semester FROM semester WHERE name = 'Semester 1')),
('c#', 'Donald Trump', 4, (SELECT id_semester FROM semester WHERE name = 'Semester 1')),
('java', 'Joe Petkov', 4.5, (SELECT id_semester FROM semester WHERE name = 'Semester 1')),
('sql', 'John Johnson', 3.5, (SELECT id_semester FROM semester WHERE name = 'Semester 1')),
('jquery', 'Peter Petrov', 5, (SELECT id_semester FROM semester WHERE name = 'Semester 1')),
('c#', 'Donald Trump', 4, (SELECT id_semester FROM semester WHERE name = 'Semester 1')),
('java', 'Joe Petkov', 4.5, (SELECT id_semester FROM semester WHERE name = 'Semester 1')),
('sql', 'John Johnson', 3.5, (SELECT id_semester FROM semester WHERE name = 'Semester 2')),
('jquery', 'Peter Petrov', 5, (SELECT id_semester FROM semester WHERE name = 'Semester 2')),
('c#', 'Donald Trump', 4, (SELECT id_semester FROM semester WHERE name = 'Semester 2')),
('java', 'Joe Petkov', 4.5, (SELECT id_semester FROM semester WHERE name = 'Semester 2')),
('sql', 'John Johnson', 3.5, (SELECT id_semester FROM semester WHERE name = 'Semester 2')),
('jquery', 'Peter Petrov', 5, (SELECT id_semester FROM semester WHERE name = 'Semester 3')),
('c#', 'Donald Trump', 4, (SELECT id_semester FROM semester WHERE name = 'Semester 3')),
('java', 'Joe Petkov', 4.5, (SELECT id_semester FROM semester WHERE name = 'Semester 3')),
('sql', 'John Johnson', null, (SELECT id_semester FROM semester WHERE name = 'Semester 3')),
('jquery', 'Peter Petrov', null, (SELECT id_semester FROM semester WHERE name = 'Semester 4')),
('c#', 'Donald Trump', null, (SELECT id_semester FROM semester WHERE name = 'Semester 4')),
('java', 'Joe Petkov', null, (SELECT id_semester FROM semester WHERE name = 'Semester 4')),
('sql', 'John Johnson', null, (SELECT id_semester FROM semester WHERE name = 'Semester 4')),
('jquery', 'Peter Petrov', null, (SELECT id_semester FROM semester WHERE name = 'Semester 4')),
('c#', 'Donald Trump', null, (SELECT id_semester FROM semester WHERE name = 'Semester 4')),
('java', 'Joe Petkov', null, (SELECT id_semester FROM semester WHERE name = 'Semester 4')),
('sql', 'John Johnson', null, (SELECT id_semester FROM semester WHERE name = 'Semester 4')),
('jquery', 'Peter Petrov', null, (SELECT id_semester FROM semester WHERE name = 'Semester 4')),
('c#', 'Donald Trump', null, (SELECT id_semester FROM semester WHERE name = 'Semester 4')),
('java', 'Joe Petkov', null, (SELECT id_semester FROM semester WHERE name = 'Semester 4')),
('sql', 'John Johnson', null, (SELECT id_semester FROM semester WHERE name = 'Semester 4')),
('jquery', 'Peter Petrov', null, (SELECT id_semester FROM semester WHERE name = 'Semester 4')),
('c#', 'Donald Trump', null, (SELECT id_semester FROM semester WHERE name = 'Semester 5')),
('java', 'Joe Petkov', null, (SELECT id_semester FROM semester WHERE name = 'Semester 5'));

INSERT INTO students_semesters (id_student, id_semester) 
VALUES((SELECT id_student FROM student LIMIT 1), (SELECT id_semester FROM semester WHERE name = 'Semester 1')),
((SELECT id_student FROM student LIMIT 1), (SELECT id_semester FROM semester WHERE name = 'Semester 2')),
((SELECT id_student FROM student LIMIT 1), (SELECT id_semester FROM semester WHERE name = 'Semester 3')),
((SELECT id_student FROM student LIMIT 1) + 1, (SELECT id_semester FROM semester WHERE name = 'Semester 1')),
((SELECT id_student FROM student LIMIT 1) + 2, (SELECT id_semester FROM semester WHERE name = 'Semester 1')),
((SELECT id_student FROM student LIMIT 1) + 3, (SELECT id_semester FROM semester WHERE name = 'Semester 4')),
((SELECT id_student FROM student LIMIT 1) + 4, (SELECT id_semester FROM semester WHERE name = 'Semester 1')),
((SELECT id_student FROM student LIMIT 1) + 5, (SELECT id_semester FROM semester WHERE name = 'Semester 2')),
((SELECT id_student FROM student LIMIT 1) + 2, (SELECT id_semester FROM semester WHERE name = 'Semester 3')),
((SELECT id_student FROM student LIMIT 1) + 4, (SELECT id_semester FROM semester WHERE name = 'Semester 1'));