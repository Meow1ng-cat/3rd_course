-- ==============================================
-- TRAVEL BOOKING — DDL + SEED
-- ==============================================

DROP TABLE IF EXISTS booking CASCADE;
DROP TABLE IF EXISTS hotel_room CASCADE;
DROP TABLE IF EXISTS tourist_city CASCADE;
DROP TABLE IF EXISTS city CASCADE;
DROP TABLE IF EXISTS hotel CASCADE;
DROP TABLE IF EXISTS room_type CASCADE;
DROP TABLE IF EXISTS tourist CASCADE;
DROP TABLE IF EXISTS country CASCADE;

-- -------------------------
-- Страны
CREATE TABLE country (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    region TEXT
);

-- -------------------------
-- Города
CREATE TABLE city (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    country_id INT REFERENCES country(id) ON DELETE SET NULL,
    population INT
);

-- -------------------------
-- Туристы
CREATE TABLE tourist (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    birth_year INT
);

-- -------------------------
-- MtM: турист ⇄ город
CREATE TABLE tourist_city (
    tourist_id INT NOT NULL REFERENCES tourist(id) ON DELETE CASCADE,
    city_id INT NOT NULL REFERENCES city(id) ON DELETE CASCADE,
    visited_at DATE NOT NULL,
    PRIMARY KEY (tourist_id, city_id)
);

-- -------------------------
-- Отели
CREATE TABLE hotel (
    id SERIAL PRIMARY KEY,
    city_id INT REFERENCES city(id) ON DELETE SET NULL,
    name TEXT NOT NULL,
    stars INT,
    year_opened INT
);

-- -------------------------
-- Типы номеров
CREATE TABLE room_type (
    id SERIAL PRIMARY KEY,
    title TEXT NOT NULL,
    max_guests INT
);

-- -------------------------
-- MtM: отель ⇄ тип номера
CREATE TABLE hotel_room (
    hotel_id INT NOT NULL REFERENCES hotel(id) ON DELETE CASCADE,
    room_type_id INT NOT NULL REFERENCES room_type(id) ON DELETE CASCADE,
    rooms_available INT NOT NULL CHECK (rooms_available >= 0),
    PRIMARY KEY (hotel_id, room_type_id)
);

-- -------------------------
-- Бронирования
CREATE TABLE booking (
    id SERIAL PRIMARY KEY,
    tourist_id INT REFERENCES tourist(id),
    hotel_id INT REFERENCES hotel(id),
    room_type_id INT REFERENCES room_type(id),
    nights INT CHECK (nights > 0),
    check_in DATE,
    total_price NUMERIC
);

-- ======================================================
-- SEED DATA
-- ======================================================

INSERT INTO country (id, name, region) VALUES
(1, 'Италия', 'Европа'),
(2, 'Япония', 'Азия'),
(3, 'Чили', 'Южная Америка'),
(4, 'Исландия', 'Европа'),
(5, 'Неизвестная страна', NULL);

INSERT INTO city (id, name, country_id, population) VALUES
(1, 'Рим', 1, 2800000),
(2, 'Милан', 1, 1400000),
(3, 'Токио', 2, 14000000),
(4, 'Саппоро', 2, 1900000),
(5, 'Сантьяго', 3, 5600000),
(6, 'Пунта-Аренас', 3, 130000),
(7, 'Рейкьявик', 4, 150000),
(8, 'Город-призрак', NULL, NULL);

INSERT INTO tourist (id, name, birth_year) VALUES
(1, 'Александр', 1990),
(2, 'Марина', 1985),
(3, 'Роберт', 1975),
(4, 'Турист без городов', 2000);

INSERT INTO tourist_city (tourist_id, city_id, visited_at) VALUES
(1, 1, '2022-05-10'),
(1, 3, '2023-11-01'),
(2, 2, '2024-02-12'),
(2, 4, '2025-01-15'),
(3, 5, '2025-03-18');

INSERT INTO hotel (id, city_id, name, stars, year_opened) VALUES
(1, 1, 'Roma Center Hotel', 5, 1990),
(2, 1, 'Budget Inn Rome', 3, 2005),
(3, 3, 'Tokyo Sky Hotel', 4, 2012),
(4, 3, 'Tiny Capsule Hotel', 2, 2018),
(5, 7, 'IceView Hotel', 4, 2020),
(6, NULL, 'Hotel Nowhere', 1, 2000);

INSERT INTO room_type (id, title, max_guests) VALUES
(1, 'Standard', 2),
(2, 'Deluxe', 3),
(3, 'Suite', 4),
(4, 'Capsule', 1);

INSERT INTO hotel_room (hotel_id, room_type_id, rooms_available) VALUES
(1, 1, 10),
(1, 2, 5),
(2, 1, 30),
(3, 1, 50),
(3, 3, 10),
(4, 4, 100),
(5, 3, 3);

INSERT INTO booking (tourist_id, hotel_id, room_type_id, nights, check_in, total_price) VALUES
(1, 1, 2, 3, '2025-10-10', 420),
(1, 3, 1, 5, '2025-11-15', 700),
(2, 4, 4, 2, '2025-09-01', 180),
(3, 5, 3, 1, '2025-07-22', 300);

-- 1.1
SELECT
    h.name AS hotel_name,
    c.name AS city_name,
    c.region
FROM
    hotel h
JOIN
    city c ON h.city_id = c.id
JOIN
    country co ON c.country_id = co.id;

-- 1.2
SELECT
    t.name AS tourist_name,
    c.name AS city_name,
    tc.visited_at
FROM
    tourist t
JOIN
    tourist_city tc ON t.id = tc.tourist_id
JOIN
    city c ON tc.city_id = c.id;

-- 2.1
SELECT
    c.name AS city_name,
    co.name AS country_name,
    h.name AS hotel_name
FROM
    city c
LEFT JOIN
    hotel h ON c.id = h.city_id
LEFT JOIN
    country co ON c.country_id = co.id;

-- 2.2
SELECT
    t.name AS tourist_name,
    COUNT(tc.city_id) AS visited_cities_count
FROM
    tourist t
LEFT JOIN
    tourist_city tc ON t.id = tc.tourist_id
GROUP BY
    t.id, t.name;

-- 3.1
SELECT
    co.name AS country_name,
    c.name AS city_name
FROM
    country co
RIGHT JOIN
    city c ON co.id = c.country_id;

-- 3.2
SELECT
    rt.title AS room_type,
    COUNT(hotel_id) AS hotel_count
FROM
    room_type rt
LEFT JOIN
    hotel_room hr ON rt.id = hr.room_type_id
GROUP BY
    rt.id, rt.title;

-- 4.1
SELECT
    c.name AS city_name,
    h.name AS hotel_name
FROM
    city c
FULL JOIN
    hotel h ON c.id = h.city_id;

-- 4.2
SELECT
    rt.title AS room_type,
    h.name AS hotel_name
FROM
    room_type rt
FULL JOIN
    hotel h
LEFT JOIN
    hotel_room hr ON h.id = hr.hotel_id AND hr.room_type_id = rt.id
ON
    TRUE
ORDER BY
    rt.title, h.name;

-- 5.1
SELECT
    co.name AS country_name,
    rt.title AS room_type
FROM
    country co
CROSS JOIN
    room_type rt;

-- 5.2
SELECT DISTINCT
    c.name AS city_name,
    h.year_opened
FROM
    city c
CROSS JOIN
    hotel h
WHERE
    h.year_opened IS NOT NULL;

-- 6.1
SELECT
    c.name AS city_name,
    h.name AS hotel_name,
    total_rooms
FROM
    city c
LEFT JOIN LATERAL (
    SELECT
        h2.id,
        h2.name,
       SUM(hr.rooms_available) AS total_rooms
    FROM
        hotel h2
    JOIN
        hotel_room hr ON h2.id = hr.hotel_id
    WHERE
        h2.city_id = c.id
    GROUP BY
        h2.id, h2.name
    ORDER BY
        total_rooms DESC
    LIMIT 1
) h ON TRUE;

-- 6.2
SELECT
    t.name AS tourist_name,
    tc.visited_at AS last_visit_date
FROM
    tourist t
LEFT JOIN LATERAL (
    SELECT
        visited_at
    FROM
        tourist_city tc2
    WHERE
        tc2.tourist_id = t.id
    ORDER BY
        visited_at DESC
    LIMIT 1
) tc ON TRUE;

-- 7.1
SELECT
    c1.name AS city1,
    c2.name AS city2
FROM
    city c1
JOIN
    city c2 ON c1.id < c2.id AND c1.country_id = c2.country_id
WHERE
    c1.id != c2.id;

-- 7.2
SELECT
    t1.name AS tourist1,
    t2.name AS tourist2,
    t1.birth_year
FROM
    tourist t1
JOIN
    tourist t2 ON t1.id < t2.id AND t1.birth_year = t2.birth_year;
