CREATE TABLE books (
    id SERIAL PRIMARY KEY,
    title TEXT NOT NULL,
    author TEXT NOT NULL,
    genre TEXT NOT NULL,
    price NUMERIC(10, 2) NOT NULL CHECK (price >= 0),
    published_date DATE NOT NULL,
    CONSTRAINT unique_title_author UNIQUE (title, author)
);

INSERT INTO books (title, author, genre, price, published_date) VALUES
-- Fantasy
('The Dragon\'s Legacy', 'Jane Doe', 'Fantasy', 15.99, '2012-04-15'),
('Legend of the Dragon', 'John Smith', 'Fantasy', 22.50, '2015-07-20'),
('Mystic Dragon', 'Emily Rose', 'Fantasy', 9.50, '2010-01-01'), -- граница даты
('Dragon Tales', 'Chris Green', 'Fantasy', 12.00, '2020-12-31'), -- граница даты
('The Lost Dragon', 'Anna White', 'Fantasy', 18.00, '2018-09-05'),
('Fire and Ice', 'Liam Fox', 'Fantasy', 17.75, '2011-11-11'), -- не начинается с Dragon
('Dragons of Middle-earth', 'Tolkien', 'Fantasy', 25.00, '2013-03-01'),
('Dragon Ascension', 'Harper Lane', 'Fantasy', 16.00, '2019-06-15'),
-- Science Fiction
('Galactic Voyages', 'Isaac Asimov', 'Science Fiction', 14.99, '2005-05-20'), -- вне диапазона даты
('Space Odyssey', 'Arthur Clarke', 'Science Fiction', 18.50, '2010-01-10'), -- внутри диапазона
('Time Travelers', 'H.G. Wells', 'Science Fiction', 8.99, '1895-12-01'), -- не входит в диапазон цены
('Future Horizons', 'Mary Shelley', 'Science Fiction', 12.00, '2018-02-14'), -- внутри диапазона
('Alien Encounters', 'Philip K. Dick', 'Science Fiction', 16.50, '2014-08-22'),
('Beyond Earth', 'William Gibson', 'Science Fiction', 20.00, '2019-09-09'), -- цена вне диапазона
('The Last Robot', 'Isaac Asimov', 'Science Fiction', 19.50, '2011-03-22'),
-- Mystery
('The Hidden Clue', 'Agatha Christie', 'Mystery', 9.99, '2000-06-15'),
('Murder at Midnight', 'Arthur Conan Doyle', 'Mystery', 14.50, '2015-08-20'),
('Secrets and Lies', 'Dan Brown', 'Mystery', 11.00, '2012-01-10'),
-- Romance
('Love in Paris', 'Jane Austen', 'Romance', 8.99, '1813-01-28'), -- дата вне диапазона
('Summer Love', 'Emily Bronte', 'Romance', 10.00, '2019-07-11'),
-- Reference
('Sample Guide to Chemistry', 'Dr. Smith', 'Reference', 25.00, '1999-05-05'), -- нужно удалить
('Encyclopedia Sample', 'Jane Doe', 'Reference', 30.00, '2001-03-01'), -- дата за границами условий
('World Atlas', 'National Geographic', 'Reference', 20.00, '1995-09-09'), -- дата вне диапазона
('Sample Dictionary', 'Oxford University', 'Reference', 15.00, '1998-11-11'), -- дата до 2000-01-01, название содержит Sample
-- Другие жанры и случаи
('Mystery of the Old House', 'Charles Dickens', 'Mystery', 7.50, '1880-04-01'),
('The Great Adventure', 'Mark Twain', 'Adventure', 12.50, '1895-07-21'),
('The Secret Garden', 'Frances Hodgson Burnett', 'Fantasy', 14.00, '1911-12-25');

SELECT *
FROM books
WHERE genre ILIKE '%fantasy%'
  AND title ILIKE 'Dragon%'
  AND published_date BETWEEN '2010-01-01' AND '2020-12-31'
ORDER BY title;

UPDATE books
SET price = ROUND(price * 1.15, 2)
WHERE genre ILIKE 'Science Fiction'
  AND price BETWEEN 9.99 AND 19.99
  AND title NOT ILIKE '%Box Set%';

DELETE FROM books
WHERE genre ILIKE 'Reference'
  AND published_date < '2000-01-01'
  AND title ILIKE '%Sample%';