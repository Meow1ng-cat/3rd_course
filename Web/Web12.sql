DROP TABLE IF EXISTS orders_import_lines;
CREATE TABLE orders_import_lines (
  id serial PRIMARY KEY,
  source_file text NOT NULL,   -- имя файла/источника
  line_no int NOT NULL,        -- номер строки
  raw_line text NOT NULL,      -- необработанная строка
  imported_at timestamptz default now(),
  note text
);

INSERT INTO orders_import_lines (source_file, line_no, raw_line, note) VALUES
-- Контакты покупателей: email в угловых скобках и простые варианты, телефоны в разных форматах
('marketplace_A_2025_11.csv', 1, 'Order#1001; Customer: Olga Petrova <olga.petrova@example.com>; +7 (921) 555-12-34; Items: SKU:AB-123-XY x1', 'order row'),
('marketplace_A_2025_11.csv', 2, 'Order#1002; Customer: Ivan <ivan@@example..com>; 8-921-5551234; Items: SKU:zx9999 x2', 'order row'),
('newsletter_upload.csv', 10, 'john.doe@domain.com; +44 7700 900123; tags: promo, holiday', 'marketing upload'),

-- Цены с разделителями тысяч и валютой
('pricing_feed.csv', 3, 'product: ZX-11; price: "1,299.99" USD', 'price row'),
('pricing_feed.csv', 4, 'product: Y-200; price: "2 500,00" EUR', 'price row'),

-- Теги/категории в поле tags:
('catalog_tags.csv', 1, 'tags: electronics, mobile,  accessories', 'tags row'),
('catalog_tags.csv', 2, 'tags: home,kitchen', 'tags row'),

-- «Грязные» CSV-строки: запятые внутри полей, кавычки
('orders_dirty.csv', 5, '"Smith, John","12 Baker St, Apt 4","1,200.00","SKU: AB-123-XY"', 'dirty csv'),

-- Логи обработки: разного регистра, ошибки и предупреждения
('processor_log.txt', 100, 'INFO: Processing order 1001', 'log'),
('processor_log.txt', 101, 'warning: price parse failed for line 4', 'log'),
('processor_log.txt', 102, 'Error: invalid phone for order 1002', 'log'),
('processor_log.txt', 103, 'error: missing sku in items list', 'log'),

-- Ловушки / edge-cases для проверки наивных regex
('marketplace_A_2025_11.csv', 20, 'Customer: bad@-domain.com; +7 921 ABC-12-34; Items: SKU: 12-AB-!!', 'trap-invalid-email-phone-sku'),
('orders_dirty.csv', 6, '"O\'Connor, Liam","New York, NY","500"', 'dirty csv with apostrophe');

-- Задание 1
SELECT id, source_file, line_no, raw_line
FROM orders_import_lines
WHERE raw_line ~* '(<[^@<>]+@[^@<>]+\.[^@<>]+>)|([^@<>]+@[^@<>]+\.[^@<>]+)';

-- Задание 2
SELECT id, source_file, line_no, raw_line
FROM orders_import_lines
WHERE raw_line !~* '(<[^@<>]+@[^@<>]+\.[^@<>]+>)|([^@<>]+@[^@<>]+\.[^@<>]+)';

-- Задание 3
SELECT id, source_file, line_no,
       (regexp_match(raw_line,
         '(<([^@<>]+@[^@<>]+\.[^@<>]+)>)|([^@<>]+@[^@<>]+\.[^@<>]+)'))[2]
       AS email
FROM orders_import_lines
WHERE raw_line ~* '(<[^@<>]+@[^@<>]+\.[^@<>]+>)|([^@<>]+@[^@<>]+\.[^@<>]+)';

-- Задание 4
SELECT id, source_file, line_no,
       regexp_matches(raw_line, '(?:SKU:\s*)?([A-Za-z0-9]{2,}-\d{1,10}(?:-[A-Za-z0-9]{2,})?)', 'g') AS sku_matches
FROM orders_import_lines
WHERE raw_line ~ '(?:SKU:\s*)?[A-Za-z0-9]{2,}-\d{1,10}(?:-[A-Za-z0-9]{2,})?';

-- Задание 5
SELECT id, source_file, line_no,
       regexp_replace(raw_line, '[^0-9]', '', 'g') AS phone_digits
FROM orders_import_lines
WHERE raw_line ~* '(\+?\d[\d\s\-$$]*)';

-- Задание 6
SELECT id, source_file, line_no,
       CAST(
         regexp_replace(
           regexp_replace(raw_line, '[^0-9,]', '', 'g'),
           '(?<=\d),(?=\d)', '.', 'g') AS numeric
       ) AS price_num
FROM orders_import_lines
WHERE raw_line ~* 'price:';

-- Задание 7
SELECT id, source_file, line_no,
       regexp_split_to_array(
         trim(both ' ' FROM substring(raw_line from 'tags:\s*(.*)$')),
         '\s*,\s*'
       ) AS tags_array
FROM orders_import_lines
WHERE raw_line ~* 'tags:';

-- Задание 8
SELECT line_no,
       regexp_split_to_table(raw_line, ',(?=(?:[^"]*"[^"]*")*[^"]*$)') AS field
FROM orders_import_lines
WHERE raw_line ~ '^".*"';

-- Задание 9
SELECT id, source_file, line_no, raw_line
FROM orders_import_lines
WHERE source_file = 'processor_log.txt' AND raw_line ~* 'error';

-- Задание 10
UPDATE orders_import_lines
SET raw_line = regexp_replace(raw_line, '(?i)error', 'ERROR', 'g')
WHERE source_file = 'processor_log.txt' AND raw_line ~* 'error';