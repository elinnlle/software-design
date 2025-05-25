CREATE SCHEMA IF NOT EXISTS analysis;

CREATE TABLE IF NOT EXISTS analysis.data (
    id UUID PRIMARY KEY,
    count_paragraphs INT NOT NULL,
    count_words INT NOT NULL,
    count_chars INT NOT NULL
);