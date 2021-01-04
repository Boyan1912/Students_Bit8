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