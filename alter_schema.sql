ALTER TABLE `mydb`.`student` 
ADD COLUMN `id_semester` INT NULL AFTER `last_name`,
ADD INDEX `fk_student_semester_idx` (`id_semester` ASC) VISIBLE;
;
ALTER TABLE `mydb`.`student` 
ADD CONSTRAINT `fk_student_semester`
  FOREIGN KEY (`id_semester`)
  REFERENCES `mydb`.`semester` (`id_semester`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
  
  
ALTER TABLE `mydb`.`discipline` 
DROP FOREIGN KEY `fk_discipline_semester`;
ALTER TABLE `mydb`.`discipline` 
ADD CONSTRAINT `fk_discipline_semester`
  FOREIGN KEY (`id_semester`)
  REFERENCES `mydb`.`semester` (`id_semester`)
  ON DELETE CASCADE;
  
  
ALTER TABLE `mydb`.`semester` 
DROP FOREIGN KEY `fk_semester_student`;
ALTER TABLE `mydb`.`semester` 
DROP COLUMN `id_student`,
ADD COLUMN `start_date` DATETIME NULL AFTER `name`,
ADD COLUMN `end_date` DATETIME NULL AFTER `start_date`,
CHANGE COLUMN `name` `name` VARCHAR(100) NOT NULL ,
DROP INDEX `fk_semester_student_idx` ;
;

ALTER TABLE `mydb`.`student` 
DROP FOREIGN KEY `fk_student_semester`;
ALTER TABLE `mydb`.`student` 
DROP COLUMN `id_semester`,
ADD COLUMN `date_of_birth` DATETIME NULL AFTER `last_name`,
DROP INDEX `fk_student_semester_idx` ;
;

CREATE TABLE `mydb`.`students_semesters` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `id_student` INT NULL,
  `id_semester` INT NULL,
  PRIMARY KEY (`id`));
