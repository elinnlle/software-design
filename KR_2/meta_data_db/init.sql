CREATE SCHEMA IF NOT EXISTS file_storage;

CREATE TABLE IF NOT EXISTS file_storage.files (
    id UUID PRIMARY KEY,
    name VARCHAR(255),
    path VARCHAR(2000),
    hash VARCHAR(64)
);