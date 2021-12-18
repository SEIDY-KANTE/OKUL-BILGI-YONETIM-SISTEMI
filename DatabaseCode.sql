
-- Table: public.adminUser

-- DROP TABLE IF EXISTS public."adminUser";

CREATE TABLE IF NOT EXISTS public."adminUser"
(
    "userId" serial,
    username character varying(10) COLLATE pg_catalog."default" NOT NULL,
    password character varying(15) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "adminUser_pkey" PRIMARY KEY ("userId"),
    CONSTRAINT "adminUser_username_key" UNIQUE (username)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."adminUser"
    OWNER to postgres;

-- Table: public.country

-- DROP TABLE IF EXISTS public.country;

CREATE TABLE IF NOT EXISTS public.country
(
    "countryId" integer NOT NULL,
    "countryName" character varying COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT country_pkey PRIMARY KEY ("countryId")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.country
    OWNER to postgres;



-- Table: public.region

-- DROP TABLE IF EXISTS public.region;

CREATE TABLE IF NOT EXISTS public.region
(
    "regionId" integer NOT NULL,
    "regionName" character varying COLLATE pg_catalog."default" NOT NULL,
    "countryId" integer NOT NULL,
    CONSTRAINT region_pkey PRIMARY KEY ("regionId"),
    CONSTRAINT country_region_fkey FOREIGN KEY ("countryId")
        REFERENCES public.country ("countryId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.region
    OWNER to postgres;
-- Index: fki_country_region_fkey

-- DROP INDEX IF EXISTS public.fki_country_region_fkey;

CREATE INDEX IF NOT EXISTS fki_country_region_fkey
    ON public.region USING btree
    ("countryId" ASC NULLS LAST)
    TABLESPACE pg_default;


	

-- Table: public.town

-- DROP TABLE IF EXISTS public.town;

CREATE TABLE IF NOT EXISTS public.town
(
    "townId" integer NOT NULL,
    "townName" character varying COLLATE pg_catalog."default" NOT NULL,
    "regionId" integer NOT NULL,
    CONSTRAINT twon_pkey PRIMARY KEY ("townId"),
    CONSTRAINT region_town_fkey FOREIGN KEY ("regionId")
        REFERENCES public.region ("regionId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.town
    OWNER to postgres;
-- Index: fki_region_twon_fkey

-- DROP INDEX IF EXISTS public.fki_region_twon_fkey;

CREATE INDEX IF NOT EXISTS fki_region_twon_fkey
    ON public.town USING btree
    ("regionId" ASC NULLS LAST)
    TABLESPACE pg_default;


-- Table: public.student

-- DROP TABLE IF EXISTS public.student;

CREATE TABLE IF NOT EXISTS public.student
(
    "stdId" integer NOT NULL,
    "firstName" character varying(20) COLLATE pg_catalog."default" NOT NULL,
    "lastName" character varying(15) COLLATE pg_catalog."default" NOT NULL,
    "dateOfBirth" date NOT NULL,
    gender character varying COLLATE pg_catalog."default" NOT NULL,
    level character varying(10) COLLATE pg_catalog."default" NOT NULL,
    photo bytea,
    "addressNo" integer,
    CONSTRAINT student_pkey PRIMARY KEY ("stdId"),
    CONSTRAINT student_address_fkey FOREIGN KEY ("addressNo")
        REFERENCES public.town ("townId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.student
    OWNER to postgres;
-- Index: fki_a

-- DROP INDEX IF EXISTS public.fki_a;

CREATE INDEX IF NOT EXISTS fki_a
    ON public.student USING btree
    ("addressNo" ASC NULLS LAST)
    TABLESPACE pg_default;

-- Trigger: totalstudent



-- Table: public.teacher

-- DROP TABLE IF EXISTS public.teacher;

CREATE TABLE IF NOT EXISTS public.teacher
(
    "teacherId" integer NOT NULL,
    "firstName" character varying(20) COLLATE pg_catalog."default" NOT NULL,
    "lastName" character varying(15) COLLATE pg_catalog."default" NOT NULL,
    "dateOfBirth" date NOT NULL,
    gender character varying COLLATE pg_catalog."default" NOT NULL,
    qualification character varying(10) COLLATE pg_catalog."default" NOT NULL,
    photo bytea,
    "addressNo" integer,
    CONSTRAINT teacher_pkey PRIMARY KEY ("teacherId"),
    CONSTRAINT teacher_twon FOREIGN KEY ("addressNo")
        REFERENCES public.town ("townId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.teacher
    OWNER to postgres;
-- Index: fki_teacher_twon

-- DROP INDEX IF EXISTS public.fki_teacher_twon;

CREATE INDEX IF NOT EXISTS fki_teacher_twon
    ON public.teacher USING btree
    ("addressNo" ASC NULLS LAST)
    TABLESPACE pg_default;




-- Table: public.contact

-- DROP TABLE IF EXISTS public.contact;

CREATE TABLE IF NOT EXISTS public.contact
(
    "contactId" serial,
    telefon character varying COLLATE pg_catalog."default",
    email character varying COLLATE pg_catalog."default",
    "personId" integer NOT NULL,
    CONSTRAINT contact_pkey PRIMARY KEY ("contactId"),
    CONSTRAINT contact_email_key UNIQUE (email),
    CONSTRAINT "contact_personId_key" UNIQUE ("personId"),
    CONSTRAINT contact_telefon_key UNIQUE (telefon),
    CONSTRAINT contact_student_fkey FOREIGN KEY ("personId")
        REFERENCES public.student ("stdId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.contact
    OWNER to postgres;
-- Index: fki_contact_student_fkey

-- DROP INDEX IF EXISTS public.fki_contact_student_fkey;

CREATE INDEX IF NOT EXISTS fki_contact_student_fkey
    ON public.contact USING btree
    ("personId" ASC NULLS LAST)
    TABLESPACE pg_default;


-- Table: public.school

-- DROP TABLE IF EXISTS public.school;

CREATE TABLE IF NOT EXISTS public.school
(
    "schoolId" serial NOT NULL,
    "schoolName" character varying COLLATE pg_catalog."default" NOT NULL,
    "addressNo" integer,
    CONSTRAINT school_pkey PRIMARY KEY ("schoolId"),
    CONSTRAINT school_town_fkey FOREIGN KEY ("addressNo")
        REFERENCES public.town ("townId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.school
    OWNER to postgres;
-- Index: fki_school_town_fkey

-- DROP INDEX IF EXISTS public.fki_school_town_fkey;

CREATE INDEX IF NOT EXISTS fki_school_town_fkey
    ON public.school USING btree
    ("schoolId" ASC NULLS LAST)
    TABLESPACE pg_default;


-- Table: public.schoolcontact

-- DROP TABLE IF EXISTS public.schoolcontact;

CREATE TABLE IF NOT EXISTS public.schoolcontact
(
    "contactId" serial NOT NULL ,
    telefon character varying COLLATE pg_catalog."default",
    email character varying COLLATE pg_catalog."default",
    "schoolId" integer NOT NULL,
    CONSTRAINT schoolcontact_pkey PRIMARY KEY ("contactId"),
    CONSTRAINT schoolcontact_email_key UNIQUE (email),
    CONSTRAINT "schoolcontact_schoolId_key" UNIQUE ("schoolId"),
    CONSTRAINT schoolcontact_telefon_key UNIQUE (telefon),
    CONSTRAINT schoolcontact_school_fkey FOREIGN KEY ("schoolId")
        REFERENCES public.school ("schoolId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.schoolcontact
    OWNER to postgres;
-- Index: fki_schoolcontact_school_fkey

-- DROP INDEX IF EXISTS public.fki_schoolcontact_school_fkey;

CREATE INDEX IF NOT EXISTS fki_schoolcontact_school_fkey
    ON public.schoolcontact USING btree
    ("schoolId" ASC NULLS LAST)
    TABLESPACE pg_default;


-- Table: public.faculty

-- DROP TABLE IF EXISTS public.faculty;

CREATE TABLE IF NOT EXISTS public.faculty
(
    "facultyId" integer NOT NULL,
    "facultyName" character varying COLLATE pg_catalog."default" NOT NULL,
    "schoolNo" integer NOT NULL,
    CONSTRAINT faculty_pkey PRIMARY KEY ("facultyId"),
    CONSTRAINT school_faculty_fkey FOREIGN KEY ("schoolNo")
        REFERENCES public.school ("schoolId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.faculty
    OWNER to postgres;
-- Index: fki_school_faculty_fkey

-- DROP INDEX IF EXISTS public.fki_school_faculty_fkey;

CREATE INDEX IF NOT EXISTS fki_school_faculty_fkey
    ON public.faculty USING btree
    ("schoolNo" ASC NULLS LAST)
    TABLESPACE pg_default;



-- Table: public.department

-- DROP TABLE IF EXISTS public.department;

CREATE TABLE IF NOT EXISTS public.department
(
    "departmentId" integer NOT NULL,
    "departmentName" character varying COLLATE pg_catalog."default" NOT NULL,
    "facultyNo" integer NOT NULL,
    CONSTRAINT department_pkey PRIMARY KEY ("departmentId"),
    CONSTRAINT faculty_department_fkey FOREIGN KEY ("facultyNo")
        REFERENCES public.faculty ("facultyId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.department
    OWNER to postgres;
-- Index: fki_faculty_department_fkey

-- DROP INDEX IF EXISTS public.fki_faculty_department_fkey;

CREATE INDEX IF NOT EXISTS fki_faculty_department_fkey
    ON public.department USING btree
    ("facultyNo" ASC NULLS LAST)
    TABLESPACE pg_default;

-- Table: public.course

-- DROP TABLE IF EXISTS public.course;

CREATE TABLE IF NOT EXISTS public.course
(
    "courseId" integer NOT NULL,
    "courseName" character varying COLLATE pg_catalog."default" NOT NULL,
    "departmentNo" integer NOT NULL,
    CONSTRAINT course_pkey PRIMARY KEY ("courseId"),
    CONSTRAINT department_course_fkey FOREIGN KEY ("departmentNo")
        REFERENCES public.department ("departmentId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.course
    OWNER to postgres;
-- Index: fki_department_course_fkey

-- DROP INDEX IF EXISTS public.fki_department_course_fkey;

CREATE INDEX IF NOT EXISTS fki_department_course_fkey
    ON public.course USING btree
    ("departmentNo" ASC NULLS LAST)
    TABLESPACE pg_default;


-- Table: public.building

-- DROP TABLE IF EXISTS public.building;

CREATE TABLE IF NOT EXISTS public.building
(
    "buildingId" serial,
    "buildingName" character varying COLLATE pg_catalog."default" NOT NULL,
    "schoolNo" integer NOT NULL,
    CONSTRAINT building_pkey PRIMARY KEY ("buildingId"),
    CONSTRAINT school_building_fkey FOREIGN KEY ("schoolNo")
        REFERENCES public.school ("schoolId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.building
    OWNER to postgres;
-- Index: fki_school_building_fkey

-- DROP INDEX IF EXISTS public.fki_school_building_fkey;

CREATE INDEX IF NOT EXISTS fki_school_building_fkey
    ON public.building USING btree
    ("schoolNo" ASC NULLS LAST)
    TABLESPACE pg_default;



-- Table: public.room

-- DROP TABLE IF EXISTS public.room;

CREATE TABLE IF NOT EXISTS public.room
(
    "roomId" integer NOT NULL,
    "roomName" character varying COLLATE pg_catalog."default" NOT NULL,
    "buildingNo" integer NOT NULL,
    CONSTRAINT room_pkey PRIMARY KEY ("roomId"),
    CONSTRAINT building_room_fkey FOREIGN KEY ("buildingNo")
        REFERENCES public.building ("buildingId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.room
    OWNER to postgres;
-- Index: fki_building_room_fkey

-- DROP INDEX IF EXISTS public.fki_building_room_fkey;

CREATE INDEX IF NOT EXISTS fki_building_room_fkey
    ON public.room USING btree
    ("buildingNo" ASC NULLS LAST)
    TABLESPACE pg_default;



-- Table: public.class

-- DROP TABLE IF EXISTS public.class;

CREATE TABLE IF NOT EXISTS public.class
(
    "classId" integer NOT NULL,
    "className" character varying COLLATE pg_catalog."default" NOT NULL,
    credit integer NOT NULL,
    "roomNo" integer NOT NULL,
    "courseNo" integer NOT NULL,
    "teacherNo" integer NOT NULL,
    description text COLLATE pg_catalog."default",
    CONSTRAINT class_pkey PRIMARY KEY ("classId"),
    CONSTRAINT room_used_for_class_unique UNIQUE ("roomNo"),
    CONSTRAINT class_course_fkey FOREIGN KEY ("courseNo")
        REFERENCES public.course ("courseId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID,
    CONSTRAINT class_room_fkey FOREIGN KEY ("roomNo")
        REFERENCES public.room ("roomId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID,
    CONSTRAINT class_teacher_fkey FOREIGN KEY ("teacherNo")
        REFERENCES public.teacher ("teacherId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.class
    OWNER to postgres;
-- Index: fki_class_course_fkey

-- DROP INDEX IF EXISTS public.fki_class_course_fkey;

CREATE INDEX IF NOT EXISTS fki_class_course_fkey
    ON public.class USING btree
    ("courseNo" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: fki_class_room_fkey

-- DROP INDEX IF EXISTS public.fki_class_room_fkey;

CREATE INDEX IF NOT EXISTS fki_class_room_fkey
    ON public.class USING btree
    ("roomNo" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: fki_class_teacher_fkey

-- DROP INDEX IF EXISTS public.fki_class_teacher_fkey;

CREATE INDEX IF NOT EXISTS fki_class_teacher_fkey
    ON public.class USING btree
    ("teacherNo" ASC NULLS LAST)
    TABLESPACE pg_default;





-- Table: public.enroll

-- DROP TABLE IF EXISTS public.enroll;

CREATE TABLE IF NOT EXISTS public.enroll
(
    "studentId" integer NOT NULL,
    "enrollmentDate" date,
    semestry character varying COLLATE pg_catalog."default" NOT NULL,
    description text COLLATE pg_catalog."default",
    "classNo" integer NOT NULL,
    "enrollmentId" integer NOT NULL,
    CONSTRAINT enroll_pkey PRIMARY KEY ("enrollmentId"),
    CONSTRAINT enroll_class FOREIGN KEY ("classNo")
        REFERENCES public.class ("classId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID,
    CONSTRAINT enroll_student_fkey FOREIGN KEY ("studentId")
        REFERENCES public.student ("stdId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.enroll
    OWNER to postgres;
-- Index: fki_enroll_class

-- DROP INDEX IF EXISTS public.fki_enroll_class;

CREATE INDEX IF NOT EXISTS fki_enroll_class
    ON public.enroll USING btree
    ("classNo" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: fki_student_enroll_fkey

-- DROP INDEX IF EXISTS public.fki_student_enroll_fkey;

CREATE INDEX IF NOT EXISTS fki_student_enroll_fkey
    ON public.enroll USING btree
    ("studentId" ASC NULLS LAST)
    TABLESPACE pg_default;


-- Table: public.personelcontact

-- DROP TABLE IF EXISTS public.personelcontact;

CREATE TABLE IF NOT EXISTS public.personelcontact
(
    "contactId" serial NOT NULL,
    telefon character varying COLLATE pg_catalog."default",
    email character varying COLLATE pg_catalog."default",
    "personId" integer NOT NULL,
    CONSTRAINT personelcontact_pkey PRIMARY KEY ("contactId"),
    CONSTRAINT personelcontact_email_key UNIQUE (email),
    CONSTRAINT "personelcontact_personId_key" UNIQUE ("personId"),
    CONSTRAINT personelcontact_telefon_key UNIQUE (telefon),
    CONSTRAINT personelcontact_teacher_fkey FOREIGN KEY ("personId")
        REFERENCES public.teacher ("teacherId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.personelcontact
    OWNER to postgres;
-- Table: public.score

-- DROP TABLE IF EXISTS public.score;

CREATE TABLE IF NOT EXISTS public.score
(
    "studentNo" integer NOT NULL,
    "studentScore" double precision,
    description text COLLATE pg_catalog."default",
    "classNo" integer NOT NULL,
    CONSTRAINT score_class FOREIGN KEY ("classNo")
        REFERENCES public.class ("classId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID,
    CONSTRAINT score_student_fkey FOREIGN KEY ("studentNo")
        REFERENCES public.student ("stdId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.score
    OWNER to postgres;
-- Index: fki_score_class

-- DROP INDEX IF EXISTS public.fki_score_class;

CREATE INDEX IF NOT EXISTS fki_score_class
    ON public.score USING btree
    ("classNo" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: fki_score_student_fkey

-- DROP INDEX IF EXISTS public.fki_score_student_fkey;

CREATE INDEX IF NOT EXISTS fki_score_student_fkey
    ON public.score USING btree
    ("studentNo" ASC NULLS LAST)
    TABLESPACE pg_default;




-- Table: public.statistic

-- DROP TABLE IF EXISTS public.statistic;

CREATE TABLE IF NOT EXISTS public.statistic
(
    total_student integer DEFAULT 0,
    female_student integer DEFAULT 0,
    male_student integer DEFAULT 0,
    total_teacher integer DEFAULT 0,
    female_teacher integer DEFAULT 0,
    male_teacher integer DEFAULT 0,
    student_enrolled integer DEFAULT 0,
    total_course integer DEFAULT 0,
    total_class integer DEFAULT 0,
    total_faculty integer DEFAULT 0,
    total_department integer DEFAULT 0,
    total_building integer DEFAULT 0,
    total_room integer DEFAULT 0,
    total_prof integer DEFAULT 0,
    total_dr integer DEFAULT 0,
    total_lec integer DEFAULT 0,
    id serial NOT NULL ,
    CONSTRAINT id_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.statistic
    OWNER to postgres;







---------------------------------------------------FUNCTION--------------------------------------------


-- FUNCTION: public.searchbuilding(character varying)

-- DROP FUNCTION IF EXISTS public.searchbuilding(character varying);

CREATE OR REPLACE FUNCTION public.searchbuilding(
	searchdata character varying)
    RETURNS TABLE(buildingid integer, buildingname character varying, schoolid integer, schoolname character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY select "buildingId", "buildingName","schoolId", "schoolName" from building inner join school on school."schoolId"=building."schoolNo"
					WHERE CONCAT(building."buildingId" ,"buildingName") LIKE '%'|| searchdata ||'%'order by building."buildingId";
END;
$BODY$;

ALTER FUNCTION public.searchbuilding(character varying)
    OWNER TO postgres;



-- FUNCTION: public.searchclass(character varying)

-- DROP FUNCTION IF EXISTS public.searchclass(character varying);

CREATE OR REPLACE FUNCTION public.searchclass(
	searchdata character varying)
    RETURNS TABLE(classid integer, classname character varying, coursename character varying, credit integer, teacherno integer, teacherfirstname character varying, teacherlastname character varying, description text) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY select "classId","className","courseName",class.credit,"teacherNo","firstName","lastName",class.description from 
					class inner join teacher on "teacherId"="teacherNo" inner join course on "courseId"="courseNo" 
					WHERE CONCAT(class."classId" ,"className") LIKE '%'|| searchdata ||'%'order by class."classId";
END;
$BODY$;

ALTER FUNCTION public.searchclass(character varying)
    OWNER TO postgres;



-- FUNCTION: public.searchcourse(character varying)

-- DROP FUNCTION IF EXISTS public.searchcourse(character varying);

CREATE OR REPLACE FUNCTION public.searchcourse(
	searchdata character varying)
    RETURNS TABLE(courseid integer, coursename character varying, departmentid integer, departmentname character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY select "courseId", "courseName","departmentId", "departmentName" from course inner join department on course."departmentNo"=department."departmentId"
					WHERE CONCAT(course."courseId" ,"courseName") LIKE '%'|| searchdata ||'%'order by course."courseId";
END;
$BODY$;

ALTER FUNCTION public.searchcourse(character varying)
    OWNER TO postgres;



-- FUNCTION: public.searchdepartment(character varying)

-- DROP FUNCTION IF EXISTS public.searchdepartment(character varying);

CREATE OR REPLACE FUNCTION public.searchdepartment(
	searchdata character varying)
    RETURNS TABLE(departmentid integer, departmentname character varying, facultyid integer, facultyname character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY select "departmentId", "departmentName","facultyId", "facultyName" from department inner join faculty on department."facultyNo"=faculty."facultyId"
					WHERE CONCAT(department."departmentId" ,"departmentName") LIKE '%'|| searchdata ||'%'order by department."departmentId";
END;
$BODY$;

ALTER FUNCTION public.searchdepartment(character varying)
    OWNER TO postgres;



-- FUNCTION: public.searchenroll(character varying)

-- DROP FUNCTION IF EXISTS public.searchenroll(character varying);

CREATE OR REPLACE FUNCTION public.searchenroll(
	searchdata character varying)
    RETURNS TABLE(enrollmentid integer, studentid integer, firstname character varying, lastname character varying, classname character varying, semestry character varying, enrollmentdate date, description text) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY select "enrollmentId","studentId","firstName","lastName","className",enroll.semestry,"enrollmentDate", enroll.description
				from enroll left join student on "stdId"="studentId" left join class on "classId"="classNo" 
				WHERE CONCAT(enroll."enrollmentId","studentId","className") LIKE '%'|| searchdata ||'%' order by enroll."enrollmentId";

END;
$BODY$;

ALTER FUNCTION public.searchenroll(character varying)
    OWNER TO postgres;


-- FUNCTION: public.searchfaculty(character varying)

-- DROP FUNCTION IF EXISTS public.searchfaculty(character varying);

CREATE OR REPLACE FUNCTION public.searchfaculty(
	searchdata character varying)
    RETURNS TABLE(facultyid integer, facultyname character varying, schoolid integer, schoolname character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY select "facultyId", "facultyName","schoolId", "schoolName" from faculty inner join school on school."schoolId"=faculty."schoolNo"
					WHERE CONCAT(faculty."facultyId" ,"facultyName") LIKE '%'|| searchdata ||'%'order by faculty."facultyId";
END;
$BODY$;

ALTER FUNCTION public.searchfaculty(character varying)
    OWNER TO postgres;



-- FUNCTION: public.searchroom(character varying)

-- DROP FUNCTION IF EXISTS public.searchroom(character varying);

CREATE OR REPLACE FUNCTION public.searchroom(
	searchdata character varying)
    RETURNS TABLE(roomid integer, roomname character varying, buildingid integer, buildingname character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY select "roomId", "roomName","buildingId", "buildingName" from room inner join building on "buildingNo"="buildingId"
					WHERE CONCAT(room."roomId" ,"roomName") LIKE '%'|| searchdata ||'%'order by room."roomId";
END;
$BODY$;

ALTER FUNCTION public.searchroom(character varying)
    OWNER TO postgres;



-- FUNCTION: public.searchschool(character varying)

-- DROP FUNCTION IF EXISTS public.searchschool(character varying);

CREATE OR REPLACE FUNCTION public.searchschool(
	searchdata character varying)
    RETURNS TABLE(schoolid integer, schoolname character varying, phone character varying, email character varying, addressno integer, townname character varying, regionname character varying, countryname character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY select school."schoolId","schoolName",schoolcontact.telefon,schoolcontact.email,school."addressNo","townName","regionName","countryName" 
				from school left join schoolcontact on school."schoolId"=schoolcontact."schoolId" left join town on school."addressNo"="townId"
				left join region on town."regionId"=region."regionId" left join country on region."countryId"=country."countryId" 
				WHERE CONCAT(school."schoolId" ,"schoolName") LIKE  '%'|| searchdata ||'%' order by school."schoolId";
END;
$BODY$;

ALTER FUNCTION public.searchschool(character varying)
    OWNER TO postgres;




-- FUNCTION: public.searchscore(character varying)

-- DROP FUNCTION IF EXISTS public.searchscore(character varying);

CREATE OR REPLACE FUNCTION public.searchscore(
	searchdata character varying)
    RETURNS TABLE(studentno integer, firstname character varying, lastname character varying, classno integer, classname character varying, studentscore double precision, teacherid integer, teacherfirstname character varying, teacherlastname character varying, description text) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY SELECT "studentNo",student."firstName", student."lastName","classNo",class."className","studentScore","teacherId",teacher."firstName",teacher."lastName",score.description 
			FROM student INNER JOIN score ON "studentNo"="stdId" inner join class ON "classNo"="classId" left join teacher ON class."teacherNo"=teacher."teacherId"  WHERE CONCAT("studentNo","classNo",student."firstName",student."lastName","studentScore") 
			LIKE '%'|| searchdata ||'%' order by "studentNo";

END;
$BODY$;

ALTER FUNCTION public.searchscore(character varying)
    OWNER TO postgres;




-- FUNCTION: public.searchstudent(character varying)

-- DROP FUNCTION IF EXISTS public.searchstudent(character varying);

CREATE OR REPLACE FUNCTION public.searchstudent(
	searchdata character varying)
    RETURNS TABLE(studentid integer, firstname character varying, fastname character varying, dateofbirth date, gender character varying, level character varying, photo bytea, phone character varying, email character varying, addressno integer, townname character varying, regionname character varying, countryname character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY select "stdId","firstName","lastName","dateOfBirth",student.gender,student.level,student.photo,contact.telefon,contact.email,student."addressNo","townName","regionName","countryName" from student left join contact on student."stdId"=contact."personId" left join town on "student"."addressNo"="townId" left join region on town."regionId"=region."regionId" left join country on region."countryId"=country."countryId" 
				WHERE CONCAT("stdId" ,"firstName","lastName") LIKE '%' || searchdata ||'%' order by "stdId";
END;
$BODY$;

ALTER FUNCTION public.searchstudent(character varying)
    OWNER TO postgres;



-- FUNCTION: public.searchstudent(character varying)

-- DROP FUNCTION IF EXISTS public.searchstudent(character varying);

CREATE OR REPLACE FUNCTION public.searchstudent(
	searchdata character varying)
    RETURNS TABLE(studentid integer, firstname character varying, fastname character varying, dateofbirth date, gender character varying, level character varying, photo bytea, phone character varying, email character varying, addressno integer, townname character varying, regionname character varying, countryname character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY select "stdId","firstName","lastName","dateOfBirth",student.gender,student.level,student.photo,contact.telefon,contact.email,student."addressNo","townName","regionName","countryName" from student left join contact on student."stdId"=contact."personId" left join town on "student"."addressNo"="townId" left join region on town."regionId"=region."regionId" left join country on region."countryId"=country."countryId" 
				WHERE CONCAT("stdId" ,"firstName","lastName") LIKE '%' || searchdata ||'%' order by "stdId";
END;
$BODY$;

ALTER FUNCTION public.searchstudent(character varying)
    OWNER TO postgres;



------------------------------------------------TRIGGER FUNCTIONS--------------------------------------

	-- FUNCTION: public.buildingRegistrationControl()

-- DROP FUNCTION IF EXISTS public."buildingRegistrationControl"();

CREATE OR REPLACE FUNCTION public."buildingRegistrationControl"()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE NOT LEAKPROOF
AS $BODY$
BEGIN

    IF NEW."schoolNo" IS NULL THEN
            RAISE EXCEPTION 'School id field cannot be empty';  
    END IF;
    RETURN NEW;
END;
$BODY$;

ALTER FUNCTION public."buildingRegistrationControl"()
    OWNER TO postgres;
	
-- Trigger: buildingRegistration

-- DROP TRIGGER IF EXISTS "buildingRegistration" ON public.building;

CREATE TRIGGER "buildingRegistration"
    BEFORE INSERT OR UPDATE 
    ON public.building
    FOR EACH ROW
    EXECUTE FUNCTION public."buildingRegistrationControl"();




-- FUNCTION: public.courseRegistrationControl()

-- DROP FUNCTION IF EXISTS public."courseRegistrationControl"();

CREATE OR REPLACE FUNCTION public."courseRegistrationControl"()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE NOT LEAKPROOF
AS $BODY$
BEGIN

    IF NEW."departmentNo" IS NULL THEN
            RAISE EXCEPTION 'Department id field cannot be empty';  
    END IF;
    RETURN NEW;
END;
$BODY$;

ALTER FUNCTION public."courseRegistrationControl"()
    OWNER TO postgres;
-- Trigger: courseRegistration

-- DROP TRIGGER IF EXISTS "courseRegistration" ON public.course;

CREATE TRIGGER "courseRegistration"
    BEFORE INSERT OR UPDATE 
    ON public.course
    FOR EACH ROW
    EXECUTE FUNCTION public."courseRegistrationControl"();



-- FUNCTION: public.departmentRegistrationControl()

-- DROP FUNCTION IF EXISTS public."departmentRegistrationControl"();

CREATE OR REPLACE FUNCTION public."departmentRegistrationControl"()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE NOT LEAKPROOF
AS $BODY$
BEGIN

    IF NEW."facultyNo" IS NULL THEN
            RAISE EXCEPTION 'Faculty id field cannot be empty';  
    END IF;
    RETURN NEW;
END;
$BODY$;

ALTER FUNCTION public."departmentRegistrationControl"()
    OWNER TO postgres;

-- Trigger: departmentRegistration

-- DROP TRIGGER IF EXISTS "departmentRegistration" ON public.department;

CREATE TRIGGER "departmentRegistration"
    BEFORE INSERT OR UPDATE 
    ON public.department
    FOR EACH ROW
    EXECUTE FUNCTION public."departmentRegistrationControl"();
	
	
	-- FUNCTION: public.regionRegistrationControl()

-- DROP FUNCTION IF EXISTS public."regionRegistrationControl"();

CREATE OR REPLACE FUNCTION public."regionRegistrationControl"()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE NOT LEAKPROOF
AS $BODY$
BEGIN

    IF NEW."countryId" IS NULL THEN
            RAISE EXCEPTION 'Country id field cannot be empty';  
    END IF;
    RETURN NEW;
END;
$BODY$;

ALTER FUNCTION public."regionRegistrationControl"()
    OWNER TO postgres;

-- Trigger: regionRegistration

-- DROP TRIGGER IF EXISTS "regionRegistration" ON public.region;

CREATE TRIGGER "regionRegistration"
    BEFORE INSERT OR UPDATE 
    ON public.region
    FOR EACH ROW
    EXECUTE FUNCTION public."regionRegistrationControl"();
	

-- FUNCTION: public.townRegistrationControl()

-- DROP FUNCTION IF EXISTS public."townRegistrationControl"();

CREATE OR REPLACE FUNCTION public."townRegistrationControl"()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE NOT LEAKPROOF
AS $BODY$
BEGIN

    IF NEW."regionId" IS NULL THEN
            RAISE EXCEPTION 'Region id field cannot be empty';  
    END IF;
    RETURN NEW;
END;
$BODY$;

ALTER FUNCTION public."townRegistrationControl"()
    OWNER TO postgres;

-- Trigger: townRegistration

-- DROP TRIGGER IF EXISTS "townRegistration" ON public.town;

CREATE TRIGGER "townRegistration"
    BEFORE INSERT OR UPDATE 
    ON public.town
    FOR EACH ROW
    EXECUTE FUNCTION public."townRegistrationControl"();



-- FUNCTION: public.facultyRegistrationControl()

-- DROP FUNCTION IF EXISTS public."facultyRegistrationControl"();

CREATE OR REPLACE FUNCTION public."facultyRegistrationControl"()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE NOT LEAKPROOF
AS $BODY$
BEGIN

    IF NEW."schoolNo" IS NULL THEN
            RAISE EXCEPTION 'School id field cannot be empty';  
    END IF;
    RETURN NEW;
END;
$BODY$;

ALTER FUNCTION public."facultyRegistrationControl"()
    OWNER TO postgres;
-- Trigger: facultyRegistration

-- DROP TRIGGER IF EXISTS "facultyRegistration" ON public.faculty;

CREATE TRIGGER "facultyRegistration"
    BEFORE INSERT OR UPDATE 
    ON public.faculty
    FOR EACH ROW
    EXECUTE FUNCTION public."facultyRegistrationControl"();
	
	
-- FUNCTION: public.numberOfStudent()

-- DROP FUNCTION IF EXISTS public."numberOfStudent"();

CREATE OR REPLACE FUNCTION public."numberOfStudent"()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE NOT LEAKPROOF
AS $BODY$
BEGIN
    IF (select count(*) from statistic)>0 THEN
       update  statistic set total_student=(select count(*) from student);
	   update  statistic set female_student=(select count(*) from student where gender='Female');
	   update  statistic set male_student=(select count(*) from student  where gender='Male');
	ELSE 
		insert into statistic(total_student) values(0);
		update  statistic set total_student=(select count(*) from student);
		update  statistic set female_student=(select count(*) from student where gender='Female');
	   	update  statistic set male_student=(select count(*) from student  where gender='Male');
    END IF;
	
    RETURN NEW;
END;
$BODY$;

ALTER FUNCTION public."numberOfStudent"()
    OWNER TO postgres;

-- DROP TRIGGER IF EXISTS totalstudent ON public.student;

CREATE TRIGGER totalstudent
    AFTER INSERT OR DELETE
    ON public.student
    FOR EACH ROW
    EXECUTE FUNCTION public."numberOfStudent"();


-- FUNCTION: public.statisticBuilding()

-- DROP FUNCTION IF EXISTS public."statisticBuilding"();

CREATE OR REPLACE FUNCTION public."statisticBuilding"()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE NOT LEAKPROOF
AS $BODY$
BEGIN
    IF (select count(*) from statistic)>0 THEN
       update  statistic set total_building=(select count(*) from building);
	  
	ELSE 
		insert into statistic(total_student) values(0);
       update  statistic set total_building=(select count(*) from building);
		
    END IF;
	
    RETURN NEW;
END;
$BODY$;

ALTER FUNCTION public."statisticBuilding"()
    OWNER TO postgres;

-- Trigger: totalbuilding

-- DROP TRIGGER IF EXISTS totalbuilding ON public.building;

CREATE TRIGGER totalbuilding
    AFTER INSERT OR DELETE
    ON public.building
    FOR EACH ROW
    EXECUTE FUNCTION public."statisticBuilding"();


-- FUNCTION: public.statisticRoom()

-- DROP FUNCTION IF EXISTS public."statisticRoom"();

CREATE OR REPLACE FUNCTION public."statisticRoom"()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE NOT LEAKPROOF
AS $BODY$
BEGIN
    IF (select count(*) from statistic)>0 THEN
       update  statistic set total_room=(select count(*) from room);
	  
	ELSE 
		insert into statistic(total_student) values(0);
       update  statistic set total_room=(select count(*) from room);
		
    END IF;
	
    RETURN NEW;
END;
$BODY$;

ALTER FUNCTION public."statisticRoom"()
    OWNER TO postgres;

-- Trigger: totalroom

-- DROP TRIGGER IF EXISTS totalroom ON public.room;

CREATE TRIGGER totalroom
    AFTER INSERT OR DELETE
    ON public.room
    FOR EACH ROW
    EXECUTE FUNCTION public."statisticRoom"();
	


-- FUNCTION: public.statisticDepartment()

-- DROP FUNCTION IF EXISTS public."statisticDepartment"();

CREATE OR REPLACE FUNCTION public."statisticDepartment"()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE NOT LEAKPROOF
AS $BODY$
BEGIN
    IF (select count(*) from statistic)>0 THEN
       update  statistic set total_department=(select count(*) from department);
	  
	ELSE 
		insert into statistic(total_student) values(0);
       update  statistic set total_department=(select count(*) from department);
		
    END IF;
	
    RETURN NEW;
END;
$BODY$;

ALTER FUNCTION public."statisticDepartment"()
    OWNER TO postgres;

-- Trigger: totaldepartment

-- DROP TRIGGER IF EXISTS totaldepartment ON public.department;

CREATE TRIGGER totaldepartment
    AFTER INSERT OR DELETE
    ON public.department
    FOR EACH ROW
    EXECUTE FUNCTION public."statisticDepartment"();
	

-- FUNCTION: public.statisticFaculty()

-- DROP FUNCTION IF EXISTS public."statisticFaculty"();

CREATE OR REPLACE FUNCTION public."statisticFaculty"()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE NOT LEAKPROOF
AS $BODY$
BEGIN
    IF (select count(*) from statistic)>0 THEN
       update  statistic set total_faculty=(select count(*) from faculty);
	  
	ELSE 
		insert into statistic(total_student) values(0);
       update  statistic set total_faculty=(select count(*) from faculty);
		
    END IF;
	
    RETURN NEW;
END;
$BODY$;

ALTER FUNCTION public."statisticFaculty"()
    OWNER TO postgres;

-- Trigger: totalfaculty

-- DROP TRIGGER IF EXISTS totalfaculty ON public.faculty;

CREATE TRIGGER totalfaculty
    AFTER INSERT OR DELETE
    ON public.faculty
    FOR EACH ROW
    EXECUTE FUNCTION public."statisticFaculty"();
	
	

-- FUNCTION: public.statisticInstructor()

-- DROP FUNCTION IF EXISTS public."statisticInstructor"();

CREATE OR REPLACE FUNCTION public."statisticInstructor"()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE NOT LEAKPROOF
AS $BODY$
BEGIN
    IF (select count(*) from statistic)>0 THEN
       update  statistic set total_teacher=(select count(*) from teacher);
	   update  statistic set female_teacher=(select count(*) from teacher where gender='Female');
	    update  statistic set male_teacher=(select count(*) from teacher where gender='Male');
	   update  statistic set total_prof=(select count(*) from teacher  where qualification='Professor');
	   update  statistic set total_dr=(select count(*) from teacher  where qualification='Doctor');
	   update  statistic set total_lec=(select count(*) from teacher  where qualification='Lector');
	ELSE 
		INSERT INTO public.statistic VALUES (0);
									 
		update  statistic set total_teacher=(select count(*) from teacher);
	   update  statistic set female_teacher=(select count(*) from teacher where gender='Female');
	    update  statistic set male_teacher=(select count(*) from teacher where gender='Male');
	    update  statistic set total_prof=(select count(*) from teacher  where qualification='Professor');
	   update  statistic set total_dr=(select count(*) from teacher  where qualification='Doctor');
	   update  statistic set total_lec=(select count(*) from teacher  where qualification='Lector');
	END IF;
    RETURN NEW;
END;
$BODY$;

ALTER FUNCTION public."statisticInstructor"()
    OWNER TO postgres;

-- Trigger: totalinstructor

-- DROP TRIGGER IF EXISTS totalinstructor ON public.teacher;

CREATE TRIGGER totalinstructor
    AFTER INSERT OR DELETE
    ON public.teacher
    FOR EACH ROW
    EXECUTE FUNCTION public."statisticInstructor"();
	
	
-- FUNCTION: public.statisticClass()

-- DROP FUNCTION IF EXISTS public."statisticClass"();

CREATE OR REPLACE FUNCTION public."statisticClass"()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE NOT LEAKPROOF
AS $BODY$
BEGIN
    IF (select count(*) from statistic)>0 THEN
       update  statistic set total_class=(select count(*) from class);
	  
	ELSE 
		insert into statistic(total_student) values(0);
       update  statistic set total_class=(select count(*) from class);
		
    END IF;
	
    RETURN NEW;
END;
$BODY$;

ALTER FUNCTION public."statisticClass"()
    OWNER TO postgres;


-- Trigger: totalclass

-- DROP TRIGGER IF EXISTS totalclass ON public.class;

CREATE TRIGGER totalclass
    AFTER INSERT OR DELETE
    ON public.class
    FOR EACH ROW
    EXECUTE FUNCTION public."statisticClass"();


-- FUNCTION: public.statisticCourse()

-- DROP FUNCTION IF EXISTS public."statisticCourse"();

CREATE OR REPLACE FUNCTION public."statisticCourse"()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE NOT LEAKPROOF
AS $BODY$
BEGIN
    IF (select count(*) from statistic)>0 THEN
       update  statistic set total_course=(select count(*) from course);
	  
	ELSE 
		insert into statistic(total_student) values(0);
       update  statistic set total_course=(select count(*) from course);
		
    END IF;
	
    RETURN NEW;
END;
$BODY$;

ALTER FUNCTION public."statisticCourse"()
    OWNER TO postgres;

-- Trigger: totalcourse

-- DROP TRIGGER IF EXISTS totalcourse ON public.course;

CREATE TRIGGER totalcourse
    AFTER INSERT OR DELETE
    ON public.course
    FOR EACH ROW
    EXECUTE FUNCTION public."statisticCourse"();
	

-- FUNCTION: public.statisticStudentEnrolled()

-- DROP FUNCTION IF EXISTS public."statisticStudentEnrolled"();

CREATE OR REPLACE FUNCTION public."statisticStudentEnrolled"()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE NOT LEAKPROOF
AS $BODY$
BEGIN
    IF (select count(*) from statistic)>0 THEN
       update  statistic set student_enrolled=(select count(*) from enroll);
	  
	ELSE 
		insert into statistic(total_student) values(0);
		update  statistic set student_enrolled=(select count(*) from enroll);
		
    END IF;
	
    RETURN NEW;
END;
$BODY$;

ALTER FUNCTION public."statisticStudentEnrolled"()
    OWNER TO postgres;


-- Trigger: totalstudentenrolled

-- DROP TRIGGER IF EXISTS totalstudentenrolled ON public.enroll;

CREATE TRIGGER totalstudentenrolled
    AFTER INSERT OR DELETE
    ON public.enroll
    FOR EACH ROW
    EXECUTE FUNCTION public."statisticStudentEnrolled"();
