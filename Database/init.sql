CREATE SCHEMA userdata;
CREATE TABLE userdata.logins (
    name VARCHAR(200) NOT NULL,
    password VARCHAR(200) NOT NULL
);

CREATE SCHEMA chengeta;
CREATE TABLE chengeta.sounds (
    time timestamp NOT NULL,
    nodeid INT NOT NULL,
    latitude INT NOT NULL,
    longitude INT NOT NULL,
    soundtype VARCHAR(200) NOT NULL,
    probability INT NOT NULL,
    soundfile VARCHAR(500) NOT NULL                         
);