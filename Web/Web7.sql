CREATE TABLE Courses (
    id INT PRIMARY KEY AUTO_INCREMENT,
    course_name VARCHAR(255) NOT NULL,
    hours INT NOT NULL CHECK (hours BETWEEN 10 AND 200),
    difficulty_level INT NOT NULL CHECK (difficulty_level BETWEEN 1 AND 5)
);

INSERT INTO Courses (course_name, hours, difficulty_level) VALUES
('Web-разработка', 120, 2),
('Кибербезопасность', 50, 3),
('Алгоритмы', 80, 4),
('Объектно-ориентированное программирование', 150, 5);