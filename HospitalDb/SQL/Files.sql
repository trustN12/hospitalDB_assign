CREATE DATABASE "HospitalDB";

CREATE TABLE Doctor(
                       Did INT PRIMARY KEY,
                       Name VARCHAR(100),
                       Specialisation VARCHAR(100)
);


CREATE TABLE Patient(
                        Pid INT IDENTITY(1,1) PRIMARY KEY,
                        Fname VARCHAR(100),
                        Lname VARCHAR(100),
                        Gender VARCHAR(10),
                        Did INT,
                        FOREIGN KEY (Did) REFERENCES Doctor(Did)
);


INSERT INTO Doctor VALUES
                       (1,'Dr Sharma','Cardiology'),
                       (2,'Dr Gupta','Neurology'),
                       (3,'Dr Dsouza','Orthopedic'),
                       (4,'Dr Roy','Dermatology')

SELECT * FROM Doctor;


-- STORED PROCEDURES --

-- Get Patients
CREATE PROCEDURE spPatient_GetAll
    AS
BEGIN
SELECT P.*, D.Name AS DoctorName
FROM Patient P
         JOIN Doctor D ON P.Did=D.Did
END



-- Insert Patient
CREATE PROCEDURE spPatient_Insert
    @Fname VARCHAR(100),
@Lname VARCHAR(100),
@Gender VARCHAR(10),
@Did INT
AS
BEGIN
INSERT INTO Patient(Fname,Lname,Gender,Did)
VALUES(@Fname,@Lname,@Gender,@Did)
END



-- Update Patient
CREATE PROCEDURE spPatient_Update
    @Pid INT,
@Fname VARCHAR(100),
@Lname VARCHAR(100),
@Gender VARCHAR(10),
@Did INT
AS
BEGIN
UPDATE Patient
SET Fname=@Fname,
    Lname=@Lname,
    Gender=@Gender,
    Did=@Did
WHERE Pid=@Pid
END



-- DElete Patient
CREATE PROCEDURE spPatient_Delete
    @Pid INT
AS
BEGIN
DELETE FROM Patient WHERE Pid=@Pid
END


-- Get Doctors
CREATE PROCEDURE spDoctor_GetAll
    AS
BEGIN
SELECT * FROM Doctor
END



---- ALL MODIFICATIONS ARE OVER HERE ----

CREATE OR ALTER PROCEDURE spPatient_GetAll
    AS
BEGIN

SELECT
    p.Pid,
    p.Fname,
    p.Lname,
    p.Gender,
    p.Did,
    d.Name AS DoctorName
FROM Patient p
         INNER JOIN Doctor d ON p.Did = d.Did

END




CREATE OR ALTER PROCEDURE spPatient_GetAll
    AS
BEGIN

SELECT
    p.Pid,
    p.Fname,
    p.Lname,
    p.Gender,
    p.Did,
    d.Name AS DoctorName,
    d.Specialisation
FROM Patient p
         INNER JOIN Doctor d ON p.Did = d.Did

END



CREATE PROCEDURE spPatient_GetById
    @Pid INT
AS
BEGIN

SELECT
    p.Pid,
    p.Fname,
    p.Lname,
    p.Gender,
    d.Name AS DoctorName,
    d.Specialisation

FROM Patient p
         INNER JOIN Doctor d ON p.Did = d.Did
WHERE p.Pid = @Pid

END



CREATE OR ALTER PROCEDURE spPatient_Search
    @SearchText NVARCHAR(100)
    AS
BEGIN

SELECT
    p.Pid,
    p.Fname,
    p.Lname,
    p.Gender,
    d.Name AS DoctorName,
    d.Specialisation
FROM Patient p
         INNER JOIN Doctor d ON p.Did = d.Did
WHERE

    CAST(p.Pid AS NVARCHAR) LIKE '%' + @SearchText + '%'
   OR p.Fname LIKE '%' + @SearchText + '%'
   OR p.Lname LIKE '%' + @SearchText + '%'
   OR p.Gender LIKE '%' + @SearchText + '%'
   OR d.Name LIKE '%' + @SearchText + '%'
   OR d.Specialisation LIKE '%' + @SearchText + '%'

END