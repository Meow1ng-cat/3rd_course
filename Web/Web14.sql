-- 1
CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    username VARCHAR(50),
    phone VARCHAR(15),
    city VARCHAR(100),
    created_at TIMESTAMP DEFAULT NOW()
);

-- 2
INSERT INTO users (username, phone, city, created_at)
SELECT
    'user' || generate_series(1, 100000),
    '79' || lpad(floor(random() * 100000000)::text, 8, '0'),
    (ARRAY['Moscow', 'Saint Petersburg', 'Novosibirsk', 'Yekaterinburg', 'Kazan'])[floor(random() * 5) + 1],
    now() - (random() * interval '365 days')
FROM generate_series(1, 100000);

-- 3
EXPLAIN ANALYZE
SELECT * FROM users WHERE phone = '79333333333';

-- 4
CREATE INDEX idx_users_phone ON users(phone);

-- 5
EXPLAIN ANALYZE
SELECT * FROM users WHERE phone = '79333333333';

-- 6
EXPLAIN ANALYZE
SELECT * FROM users WHERE city ILIKE '%Moscow%';

-- 7
CREATE EXTENSION IF NOT EXISTS pg_trgm;
CREATE INDEX idx_users_city_trgm ON users USING gin (city gin_trgm_ops);

-- 8
EXPLAIN ANALYZE
SELECT * FROM users WHERE city ILIKE '%Moscow%';

