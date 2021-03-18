-- phpMyAdmin SQL Dump
-- version 4.5.4.1deb2ubuntu2
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: May 07, 2017 at 07:07 PM
-- Server version: 5.7.18-0ubuntu0.16.04.1
-- PHP Version: 7.0.15-0ubuntu0.16.04.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `uni`
--
DROP DATABASE IF EXISTS `uni`;
CREATE DATABASE IF NOT EXISTS `uni` DEFAULT CHARACTER SET utf8 COLLATE utf8_german2_ci;
USE `uni`;

-- --------------------------------------------------------

--
-- Table structure for table `assistenten`
--

DROP TABLE IF EXISTS `assistenten`;
CREATE TABLE `assistenten` (
  `persnr` int(5) UNSIGNED NOT NULL,
  `name` varchar(30) CHARACTER SET utf8 COLLATE utf8_german2_ci NOT NULL,
  `gebdat` date DEFAULT NULL,
  `gehalt` decimal(10,2) UNSIGNED NOT NULL,
  `fachgebiet` varchar(40) CHARACTER SET utf8 COLLATE utf8_german2_ci DEFAULT NULL,
  `boss` int(5) UNSIGNED DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `assistenten`
--

INSERT INTO `assistenten` (`persnr`, `name`, `gebdat`, `gehalt`, `fachgebiet`, `boss`) VALUES
(3002, 'Platon', '1967-08-29', '1820.00', 'Ideenlehre', 2125),
(3003, 'Aristoteles', '1970-05-18', '1640.50', 'Syllogistik', 2125),
(3004, 'Wittgenstein', '1982-12-01', '1739.80', 'Sprachtheorie', 2126),
(3005, 'Rhetikus', '1985-02-23', '1599.90', 'Planetenbewegung', 2127),
(3006, 'Newton', '1976-09-19', '2015.40', 'Keplersche Gesetze', 2127),
(3007, 'Spinoza', '1990-10-06', '1460.80', 'Gott und Natur', 2134);

-- --------------------------------------------------------

--
-- Table structure for table `hoeren`
--

DROP TABLE IF EXISTS `hoeren`;
CREATE TABLE `hoeren` (
  `matrnr` int(5) UNSIGNED NOT NULL,
  `vorlnr` int(4) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `hoeren`
--

INSERT INTO `hoeren` (`matrnr`, `vorlnr`) VALUES
(27550, 4052),
(26120, 5001),
(27550, 5001),
(29120, 5001),
(29555, 5001),
(25403, 5022),
(29555, 5022),
(28106, 5041),
(29120, 5041),
(29120, 5049),
(28106, 5052),
(28106, 5216),
(28106, 5259);

-- --------------------------------------------------------

--
-- Table structure for table `professoren`
--

DROP TABLE IF EXISTS `professoren`;
CREATE TABLE `professoren` (
  `persnr` int(5) UNSIGNED NOT NULL,
  `name` varchar(30) NOT NULL,
  `gebdat` date DEFAULT NULL,
  `rang` enum('c2','c3','c4') NOT NULL,
  `gehalt` decimal(10,2) UNSIGNED NOT NULL,
  `raum` int(3) UNSIGNED ZEROFILL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `professoren`
--

INSERT INTO `professoren` (`persnr`, `name`, `gebdat`, `rang`, `gehalt`, `raum`) VALUES
(2125, 'Sokrates', '1972-04-17', 'c4', '2587.20', 226),
(2126, 'Russel', '1964-05-07', 'c4', '2339.90', 232),
(2127, 'Kopernikus', '1932-02-15', 'c3', '3105.50', 310),
(2133, 'Popper', '1956-09-19', 'c3', '2460.00', 052),
(2134, 'Augustinus', '1941-11-30', 'c3', '2162.40', 309),
(2136, 'Curie', '1971-02-11', 'c4', '3410.50', 036),
(2137, 'Kant', '1968-10-06', 'c4', '2480.70', 007);

-- --------------------------------------------------------

--
-- Table structure for table `pruefen`
--

DROP TABLE IF EXISTS `pruefen`;
CREATE TABLE `pruefen` (
  `matrnr` int(5) UNSIGNED NOT NULL,
  `vorlnr` int(4) UNSIGNED NOT NULL,
  `persnr` int(5) UNSIGNED NOT NULL,
  `note` tinyint(1) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `pruefen`
--

INSERT INTO `pruefen` (`matrnr`, `vorlnr`, `persnr`, `note`) VALUES
(24002, 5001, 2125, 3),
(24002, 5049, 2125, 3),
(25403, 5041, 2125, 2),
(26120, 4052, 2126, 4),
(26120, 5052, 2125, 5),
(26830, 5049, 2125, 2),
(26830, 5216, 2134, 2),
(27550, 4630, 2137, 2),
(27550, 5216, 2134, 5),
(28106, 4052, 2133, 5),
(28106, 5001, 2126, 1),
(28106, 5043, 2133, 3),
(29120, 5022, 2125, 2),
(29555, 4630, 2137, 5),
(29555, 5041, 2137, 5);

-- --------------------------------------------------------

--
-- Table structure for table `studenten`
--

DROP TABLE IF EXISTS `studenten`;
CREATE TABLE `studenten` (
  `matrnr` int(5) UNSIGNED NOT NULL,
  `name` varchar(30) NOT NULL,
  `gebdat` date DEFAULT NULL,
  `semester` int(2) UNSIGNED DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `studenten`
--

INSERT INTO `studenten` (`matrnr`, `name`, `gebdat`, `semester`) VALUES
(24002, 'Xenokrates', '1971-01-07', 18),
(25403, 'Jonas', '1990-02-17', 12),
(26120, 'Fichte', '1992-03-01', 10),
(26830, 'Aristoxenos', '1985-04-19', 8),
(27550, 'Schopenhauer', '1993-03-08', 6),
(28106, 'Carnap', '1992-07-31', 3),
(29120, 'Theophrastos', '1987-09-22', 2),
(29555, 'Feuerbach', '1991-12-26', 2);

-- --------------------------------------------------------

--
-- Table structure for table `voraussetzen`
--

DROP TABLE IF EXISTS `voraussetzen`;
CREATE TABLE `voraussetzen` (
  `vorgaenger` int(4) UNSIGNED NOT NULL,
  `nachfolger` int(4) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `voraussetzen`
--

INSERT INTO `voraussetzen` (`vorgaenger`, `nachfolger`) VALUES
(5001, 5041),
(5001, 5043),
(5001, 5049),
(5041, 5052),
(5043, 5052),
(5041, 5216),
(5052, 5259);

-- --------------------------------------------------------

--
-- Table structure for table `vorlesungen`
--

DROP TABLE IF EXISTS `vorlesungen`;
CREATE TABLE `vorlesungen` (
  `vorlnr` int(4) UNSIGNED NOT NULL,
  `titel` varchar(30) NOT NULL,
  `sws` int(2) UNSIGNED DEFAULT NULL,
  `gelesenvon` int(5) UNSIGNED DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `vorlesungen`
--

INSERT INTO `vorlesungen` (`vorlnr`, `titel`, `sws`, `gelesenvon`) VALUES
(4052, 'Logik', 4, 2125),
(4630, 'Die drei Kritiken', 4, 2137),
(5001, 'Grundzuege', 4, 2137),
(5022, 'Glaube und Wissen', 2, 2134),
(5041, 'Ethik', 4, 2125),
(5043, 'Erkenntnistheorie', 3, 2126),
(5049, 'Maeeutik', 2, 2125),
(5052, 'Wissenschaftstheorie', 3, 2126),
(5216, 'Bioethik', 2, 2126),
(5259, 'Der Wiener Kreis', 2, 2133);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `assistenten`
--
ALTER TABLE `assistenten`
  ADD PRIMARY KEY (`persnr`),
  ADD KEY `boss` (`boss`);

--
-- Indexes for table `hoeren`
--
ALTER TABLE `hoeren`
  ADD PRIMARY KEY (`matrnr`,`vorlnr`),
  ADD KEY `vorlnr` (`vorlnr`);

--
-- Indexes for table `professoren`
--
ALTER TABLE `professoren`
  ADD PRIMARY KEY (`persnr`);

--
-- Indexes for table `pruefen`
--
ALTER TABLE `pruefen`
  ADD PRIMARY KEY (`matrnr`,`vorlnr`),
  ADD KEY `persnr` (`persnr`),
  ADD KEY `vorlnr` (`vorlnr`);

--
-- Indexes for table `studenten`
--
ALTER TABLE `studenten`
  ADD PRIMARY KEY (`matrnr`);

--
-- Indexes for table `voraussetzen`
--
ALTER TABLE `voraussetzen`
  ADD PRIMARY KEY (`vorgaenger`,`nachfolger`),
  ADD KEY `nachfolger` (`nachfolger`);

--
-- Indexes for table `vorlesungen`
--
ALTER TABLE `vorlesungen`
  ADD PRIMARY KEY (`vorlnr`),
  ADD KEY `gelesenvon` (`gelesenvon`);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `assistenten`
--
ALTER TABLE `assistenten`
  ADD CONSTRAINT `assistenten_ibfk_1` FOREIGN KEY (`boss`) REFERENCES `professoren` (`persnr`) ON UPDATE CASCADE;

--
-- Constraints for table `hoeren`
--
ALTER TABLE `hoeren`
 ADD CONSTRAINT `hoeren_ibfk_1` FOREIGN KEY (`matrnr`) REFERENCES `studenten` (`matrnr`) ON UPDATE CASCADE,
 ADD CONSTRAINT `hoeren_ibfk_2` FOREIGN KEY (`vorlnr`) REFERENCES `vorlesungen` (`vorlnr`) ON UPDATE CASCADE;

--
-- Constraints for table `pruefen`
--
ALTER TABLE `pruefen`
  ADD CONSTRAINT `pruefen_ibfk_1` FOREIGN KEY (`matrnr`) REFERENCES `studenten` (`matrnr`) ON UPDATE CASCADE,
  ADD CONSTRAINT `pruefen_ibfk_2` FOREIGN KEY (`vorlnr`) REFERENCES `vorlesungen` (`vorlnr`) ON UPDATE CASCADE,
  ADD CONSTRAINT `pruefen_ibfk_3` FOREIGN KEY (`persnr`) REFERENCES `professoren` (`persnr`) ON UPDATE CASCADE;

--
-- Constraints for table `vorlesungen`
--
ALTER TABLE `vorlesungen`
  ADD CONSTRAINT `vorlesungen_ibfk_1` FOREIGN KEY (`gelesenvon`) REFERENCES `professoren` (`persnr`) ON UPDATE CASCADE;

--
-- Constraints for table `voraussetzen`
--
ALTER TABLE `voraussetzen`
  ADD CONSTRAINT `voraussetzen_ibfk_1` FOREIGN KEY (`vorgaenger`) REFERENCES `vorlesungen` (`vorlnr`) ON UPDATE CASCADE,
  ADD CONSTRAINT `voraussetzen_ibfk_2` FOREIGN KEY (`nachfolger`) REFERENCES `vorlesungen` (`vorlnr`) ON UPDATE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
