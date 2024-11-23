-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema bhd
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema bhd
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `bhd` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci ;
USE `bhd` ;

-- -----------------------------------------------------
-- Table `bhd`.`users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `bhd`.`users` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(200) CHARACTER SET 'utf8mb3' NOT NULL,
  `email` VARCHAR(200) CHARACTER SET 'utf8mb3' NOT NULL,
  `psswd` VARCHAR(200) CHARACTER SET 'utf8mb3' NOT NULL,
  `creado` DATETIME NOT NULL,
  `modificado` DATETIME NOT NULL,
  `ultimo` DATETIME NOT NULL,
  `token` VARCHAR(200) CHARACTER SET 'utf8mb3' NOT NULL,
  `activo` TINYINT(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  UNIQUE INDEX `email_UNIQUE` (`email` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `bhd`.`phones`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `bhd`.`phones` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `userId` INT NOT NULL,
  `number` VARCHAR(45) CHARACTER SET 'utf8mb3' NOT NULL,
  `ciudadCode` VARCHAR(45) CHARACTER SET 'utf8mb3' NOT NULL,
  `paisCode` VARCHAR(45) CHARACTER SET 'utf8mb3' NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  INDEX `_idx` (`userId` ASC) VISIBLE,
  CONSTRAINT ``
    FOREIGN KEY (`userId`)
    REFERENCES `bhd`.`users` (`id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
