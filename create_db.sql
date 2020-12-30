-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`student`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`student` (
  `id_student` INT NOT NULL AUTO_INCREMENT,
  `first_name` VARCHAR(45) NULL,
  `last_name` VARCHAR(45) NULL,
  PRIMARY KEY (`id_student`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`semester`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`semester` (
  `id_semester` INT NOT NULL AUTO_INCREMENT,
  `id_student` INT NULL,
  `name` VARCHAR(100) NULL,
  PRIMARY KEY (`id_semester`),
  INDEX `fk_semester_student_idx` (`id_student` ASC) VISIBLE,
  CONSTRAINT `fk_semester_student`
    FOREIGN KEY (`id_student`)
    REFERENCES `mydb`.`student` (`id_student`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`discipline`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`discipline` (
  `id_discipline` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `professor_name` VARCHAR(100) NULL,
  `score` FLOAT NULL,
  `id_semester` INT NULL,
  PRIMARY KEY (`id_discipline`),
  INDEX `fk_discipline_semester_idx` (`id_semester` ASC) VISIBLE,
  CONSTRAINT `fk_discipline_semester`
    FOREIGN KEY (`id_semester`)
    REFERENCES `mydb`.`semester` (`id_semester`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
