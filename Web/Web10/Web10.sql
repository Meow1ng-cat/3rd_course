CREATE TABLE DIRECTOR (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) UNIQUE NOT NULL,
    country VARCHAR(100)
);

CREATE TABLE FILM (
    id SERIAL PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    release_year INT NOT NULL CHECK (release_year BETWEEN 1900 AND EXTRACT(YEAR FROM CURRENT_DATE)),
    primary_director_id INT NOT NULL,
    CONSTRAINT fk_primary_director FOREIGN KEY (primary_director_id)
        REFERENCES DIRECTOR(id) ON DELETE RESTRICT
);

CREATE TABLE FILM_INFO (
    film_id INT PRIMARY KEY,
    duration_minutes INT NOT NULL CHECK (duration_minutes > 0),
    rating VARCHAR(10) NOT NULL CHECK (rating IN ('G','PG','PG-13','R','NC-17')),
    budget_usd NUMERIC(15,2),
    CONSTRAINT fk_film_info FOREIGN KEY (film_id)
        REFERENCES FILM(id) ON DELETE CASCADE
);

CREATE TABLE FILM_CREDIT (
    film_id INT NOT NULL,
    director_id INT NOT NULL,
    role VARCHAR(50) NOT NULL,
    PRIMARY KEY (film_id, director_id, role),
    CONSTRAINT fk_film_credit_film FOREIGN KEY (film_id)
        REFERENCES FILM(id) ON DELETE CASCADE,
    CONSTRAINT fk_film_credit_director FOREIGN KEY (director_id)
        REFERENCES DIRECTOR(id) ON DELETE NO ACTION
);