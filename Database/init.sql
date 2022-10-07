CREATE SCHEMA userdata;
CREATE TABLE userdata.logins (
    name VARCHAR(200) NOT NULL,
    password VARCHAR(200) NOT NULL
);

CREATE SCHEMA chengeta;
CREATE TABLE chengeta.sounds (
    name VARCHAR(200) NOT NULL,
    value INT NOT NULL,
    timestamp timestamptz NOT NULL
);