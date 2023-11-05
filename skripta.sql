-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema saobracajnapolicija
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema saobracajnapolicija
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `saobracajnapolicija` DEFAULT CHARACTER SET utf8 ;
USE `saobracajnapolicija` ;

-- -----------------------------------------------------
-- Table `saobracajnapolicija`.`tema`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `saobracajnapolicija`.`tema` (
  `idTema` INT NOT NULL AUTO_INCREMENT,
  `naziv` VARCHAR(45) NOT NULL,
  `bojaPozadine` VARCHAR(45) NOT NULL,
  `bojaTeksta` VARCHAR(45) NOT NULL,
  `font` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idTema`),
  UNIQUE INDEX `naziv_UNIQUE` (`naziv` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 3
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `saobracajnapolicija`.`administrator`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `saobracajnapolicija`.`administrator` (
  `idAdministator` INT NOT NULL,
  `username` VARCHAR(45) NOT NULL,
  `password` VARCHAR(45) NOT NULL,
  `idTema` INT NOT NULL,
  PRIMARY KEY (`idAdministator`),
  INDEX `idTema_idx` (`idTema` ASC) VISIBLE,
  CONSTRAINT `idTema`
    FOREIGN KEY (`idTema`)
    REFERENCES `saobracajnapolicija`.`tema` (`idTema`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `saobracajnapolicija`.`policajac`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `saobracajnapolicija`.`policajac` (
  `JMB` VARCHAR(13) NOT NULL,
  `Ime` VARCHAR(45) NOT NULL,
  `Prezime` VARCHAR(45) NOT NULL,
  `Cin` VARCHAR(45) NOT NULL,
  `username` VARCHAR(45) NOT NULL,
  `password` VARCHAR(45) NOT NULL,
  `idTema` INT NOT NULL DEFAULT '1',
  `prvaPrijava` INT NULL DEFAULT '1',
  PRIMARY KEY (`JMB`),
  UNIQUE INDEX `username_UNIQUE` (`username` ASC) VISIBLE,
  INDEX `idTema_idx` (`idTema` ASC) VISIBLE,
  CONSTRAINT `kljuc`
    FOREIGN KEY (`idTema`)
    REFERENCES `saobracajnapolicija`.`tema` (`idTema`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `saobracajnapolicija`.`kazneninalog`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `saobracajnapolicija`.`kazneninalog` (
  `idKazneniNalog` VARCHAR(12) NOT NULL,
  `pocinilacJMB` VARCHAR(13) NOT NULL,
  `izvjestaj` TEXT NOT NULL,
  `vrijeme` DATETIME NOT NULL,
  `pocinilacIme` VARCHAR(45) NOT NULL,
  `pocinilacPrezime` VARCHAR(45) NOT NULL,
  `policajacJMB` VARCHAR(13) NOT NULL,
  PRIMARY KEY (`idKazneniNalog`),
  INDEX `fk_KazneniNalog_Policajac1_idx` (`policajacJMB` ASC) VISIBLE,
  CONSTRAINT `fk_KazneniNalog_Policajac1`
    FOREIGN KEY (`policajacJMB`)
    REFERENCES `saobracajnapolicija`.`policajac` (`JMB`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `saobracajnapolicija`.`radarskakazna`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `saobracajnapolicija`.`radarskakazna` (
  `idRadarskaKazna` INT NOT NULL AUTO_INCREMENT,
  `registracijskeTablice` VARCHAR(15) NOT NULL,
  `prekoracenjeBrzine` INT NOT NULL,
  `novcanaKazna` INT NOT NULL,
  `vrijemePreksaja` DATETIME NOT NULL,
  `policajacJMB` VARCHAR(13) NOT NULL,
  PRIMARY KEY (`idRadarskaKazna`),
  INDEX `fk_RadarskaKazna_Policajac1_idx` (`policajacJMB` ASC) VISIBLE,
  CONSTRAINT `fk_RadarskaKazna_Policajac1`
    FOREIGN KEY (`policajacJMB`)
    REFERENCES `saobracajnapolicija`.`policajac` (`JMB`))
ENGINE = InnoDB
AUTO_INCREMENT = 9
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `saobracajnapolicija`.`saobracajnanesreca`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `saobracajnapolicija`.`saobracajnanesreca` (
  `idSaobracajnaNesreca` INT NOT NULL AUTO_INCREMENT,
  `naslov` VARCHAR(150) NOT NULL,
  `izvjestaj` TEXT NOT NULL,
  `adresa` VARCHAR(100) NOT NULL,
  `vrijeme` DATETIME NOT NULL,
  `policajacJMB` VARCHAR(13) NOT NULL,
  PRIMARY KEY (`idSaobracajnaNesreca`),
  INDEX `fk_SaobracajnaNesreca_Policajac1_idx` (`policajacJMB` ASC) VISIBLE,
  CONSTRAINT `fk_SaobracajnaNesreca_Policajac1`
    FOREIGN KEY (`policajacJMB`)
    REFERENCES `saobracajnapolicija`.`policajac` (`JMB`))
ENGINE = InnoDB
AUTO_INCREMENT = 7
DEFAULT CHARACTER SET = utf8mb3;

USE `saobracajnapolicija`;

DELIMITER $$
USE `saobracajnapolicija`$$
CREATE
DEFINER=`root`@`localhost`
TRIGGER `saobracajnapolicija`.`AzururanjeNovcanaNadoknadaRadarskeKazne`
BEFORE UPDATE ON `saobracajnapolicija`.`radarskakazna`
FOR EACH ROW
set new.novcanaKazna=new.prekoracenjeBrzine*20$$

USE `saobracajnapolicija`$$
CREATE
DEFINER=`root`@`localhost`
TRIGGER `saobracajnapolicija`.`novcanaNadoknadaRadarskeKazne`
BEFORE INSERT ON `saobracajnapolicija`.`radarskakazna`
FOR EACH ROW
set new.novcanaKazna=new.prekoracenjeBrzine*20$$

USE `saobracajnapolicija`$$
CREATE
DEFINER=`root`@`localhost`
TRIGGER `saobracajnapolicija`.`provjeraUslovaRadarskeKazne`
BEFORE INSERT ON `saobracajnapolicija`.`radarskakazna`
FOR EACH ROW
begin
    if (new.prekoracenjeBrzine<10) then
		signal sqlstate '45000' set message_text='Kaznjiva su samo prekoracenja iznad 10 km/h';
	end if;
end$$


DELIMITER ;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
