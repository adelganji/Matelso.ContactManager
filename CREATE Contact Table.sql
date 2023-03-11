-- This script creates a Contact table in the ContactProject database
-- Please execute this script in a PostgreSQL database and update the connectionString located in Web > appsettings.json > ConnectionStrings > DefaultConnection accordingly.

CREATE TABLE contact (
    id SERIAL PRIMARY KEY,
    salution VARCHAR(255) NOT NULL,
    firstname VARCHAR(255) NOT NULL,
    lastname VARCHAR(255) NOT NULL,
    displayname VARCHAR(255),
    birthdate DATE,
    creationtimestamp TIMESTAMP DEFAULT NOW() NOT NULL,
    lastchangetimestamp TIMESTAMP,
    email VARCHAR(255) UNIQUE NOT NULL,
    phonenumber VARCHAR(20)
);
