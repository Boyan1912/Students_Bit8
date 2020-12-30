INSERT INTO mydb.student (first_name, last_name)
VALUES ('Ivan', 'Petrov'),
('Stoyan', 'Ivanov'),
('Georgi', 'Georgiev'),
('Hristo', 'Dimitrov'),
('Kalpazan', 'Petrov'),
('Ivan', 'Ivanov');

INSERT INTO mydb.semester (name, id_student)
VALUES ('spring', 1),
('summer', 2),
('autumn', 3),
('winter', 4);

INSERT INTO mydb.discipline (name, professor_name, score, id_semester)
VALUES ('sql', 'John Johnson', 3.5, 1),
('jquery', 'Peter Petrov', 5, 2),
('c#', 'Donald Trump', 4, 3),
('java', 'Joe Petkov', 4.5, 4),
('sql', 'John Johnson', 3.5, 2),
('jquery', 'Peter Petrov', 5, 3),
('c#', 'Donald Trump', 4, 4),
('java', 'Joe Petkov', 4.5, 1),
('sql', 'John Johnson', 3.5, 3),
('jquery', 'Peter Petrov', 5, 4),
('c#', 'Donald Trump', 4, 1),
('java', 'Joe Petkov', 4.5, 2),
('sql', 'John Johnson', 3.5, 4),
('jquery', 'Peter Petrov', 5, 1),
('c#', 'Donald Trump', 4, 2),
('java', 'Joe Petkov', 4.5, 3);