-- phpMyAdmin SQL Dump
-- version 4.6.6deb5
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Jul 17, 2019 at 02:51 PM
-- Server version: 5.7.26-0ubuntu0.18.04.1
-- PHP Version: 7.2.19-0ubuntu0.18.04.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `fitness`
--

-- --------------------------------------------------------

--
-- Table structure for table `abhaltung`
--

CREATE TABLE `abhaltung` (
  `trainer` int(10) UNSIGNED NOT NULL,
  `kursnr` int(10) UNSIGNED NOT NULL,
  `raumrnr` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_german2_ci;

--
-- Dumping data for table `abhaltung`
--

INSERT INTO `abhaltung` (`trainer`, `kursnr`, `raumrnr`) VALUES
(2323, 1, 25),
(7777, 2, 30),
(7777, 3, 30),
(2222, 4, 22),
(1818, 5, 22),
(1010, 6, 29),
(6666, 8, 26),
(6666, 9, 27),
(2222, 10, 23),
(2323, 11, 25),
(7777, 12, 31),
(1515, 13, 22),
(2121, 14, 24),
(1818, 15, 22),
(4444, 16, 23),
(6666, 17, 27),
(9999, 17, 31),
(1919, 19, 22),
(2323, 20, 25),
(1717, 21, 31),
(8888, 22, 28),
(1818, 23, 22),
(6666, 24, 27),
(2020, 25, 31),
(2222, 26, 23);

-- --------------------------------------------------------

--
-- Table structure for table `abonnement`
--

CREATE TABLE `abonnement` (
  `knr` int(10) UNSIGNED NOT NULL,
  `ablaufdat` date NOT NULL,
  `gebuehr` decimal(7,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_german2_ci;

--
-- Dumping data for table `abonnement`
--

INSERT INTO `abonnement` (`knr`, `ablaufdat`, `gebuehr`) VALUES
(1, '2020-01-01', '149.70'),
(5, '2021-01-01', '99.80'),
(7, '2020-02-11', '149.70'),
(9, '2019-12-12', '49.90'),
(10, '2021-05-01', '99.80'),
(13, '2021-05-01', '149.70'),
(19, '2018-01-01', '49.90'),
(20, '2019-02-12', '99.80');

-- --------------------------------------------------------

--
-- Table structure for table `administration`
--

CREATE TABLE `administration` (
  `svnr` int(10) UNSIGNED NOT NULL,
  `funktion` varchar(40) COLLATE utf8_german2_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_german2_ci;

--
-- Dumping data for table `administration`
--

INSERT INTO `administration` (`svnr`, `funktion`) VALUES
(1111, 'Kundenbetreuung'),
(1212, 'Kundenbetreuung'),
(1313, 'Geraetebetreuung'),
(1414, 'Kundenbetreuung'),
(3333, 'Sekretariat'),
(5555, 'Geraetebetreuung');

-- --------------------------------------------------------

--
-- Table structure for table `arbeitet`
--

CREATE TABLE `arbeitet` (
  `svnr` int(10) UNSIGNED NOT NULL,
  `fcnr` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_german2_ci;

--
-- Dumping data for table `arbeitet`
--

INSERT INTO `arbeitet` (`svnr`, `fcnr`) VALUES
(1010, 1),
(1111, 1),
(1212, 1),
(1313, 1),
(1414, 1),
(1515, 1),
(1616, 1),
(1717, 1),
(1818, 1),
(1919, 1),
(2020, 1),
(2222, 1),
(3333, 1),
(4444, 1),
(5555, 1),
(6666, 1),
(7777, 1),
(8888, 1),
(9999, 1),
(1111, 2),
(2020, 2),
(2323, 2),
(4444, 2),
(5555, 2),
(6666, 2),
(7777, 2),
(1010, 3),
(1111, 3),
(1212, 3),
(2020, 3),
(2121, 3),
(6666, 3),
(8888, 3),
(9191, 3),
(1111, 4),
(1212, 4),
(1313, 4),
(2020, 4),
(2121, 4),
(2323, 4),
(6666, 4),
(9191, 4),
(1010, 5),
(1111, 5),
(1212, 5),
(1313, 5),
(1414, 5),
(1515, 5),
(1616, 5),
(1717, 5),
(1818, 5),
(1919, 5),
(2020, 5),
(5555, 5),
(6666, 5),
(7777, 5),
(8888, 5),
(9191, 5),
(9999, 5);

-- --------------------------------------------------------

--
-- Table structure for table `einzeltraining`
--

CREATE TABLE `einzeltraining` (
  `trainer` int(10) UNSIGNED NOT NULL,
  `kundennr` int(10) UNSIGNED NOT NULL,
  `schwerpunkt` varchar(50) COLLATE utf8_german2_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_german2_ci;

--
-- Dumping data for table `einzeltraining`
--

INSERT INTO `einzeltraining` (`trainer`, `kundennr`, `schwerpunkt`) VALUES
(1010, 6, 'Powerplatetraining'),
(1010, 10, 'Salsa'),
(1818, 3, 'Staerkung der Wirbelsaeule'),
(1818, 13, 'Bauchmuskeltraining'),
(2323, 2, 'Woyo'),
(2323, 6, 'Yogalates'),
(7777, 6, 'Salsa'),
(7777, 10, 'Salsa'),
(7777, 11, 'Bauchtanz'),
(7777, 13, 'Salsa');

-- --------------------------------------------------------

--
-- Table structure for table `fitnesscenter`
--

CREATE TABLE `fitnesscenter` (
  `nr` int(10) UNSIGNED NOT NULL,
  `name` varchar(50) COLLATE utf8_german2_ci DEFAULT NULL,
  `email` varchar(50) COLLATE utf8_german2_ci DEFAULT NULL,
  `telnr` varchar(10) COLLATE utf8_german2_ci DEFAULT NULL,
  `leiter` int(10) UNSIGNED DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_german2_ci;

--
-- Dumping data for table `fitnesscenter`
--

INSERT INTO `fitnesscenter` (`nr`, `name`, `email`, `telnr`, `leiter`) VALUES
(1, 'Healthclub Fitgasse', 'fitgasse@fit.at', '51349872', 1111),
(2, 'Healthclub Wegmitdemspeck-platz', 'wegmitdemspeck@fit.at', '41965470', 5555),
(3, 'Healthclub Fatburningstrasse', 'fatburningstrasse@fit.at', '65497315', 2121),
(4, 'Healthclub Fitnessgasse', 'fitnessgasse@fit.at', '30978551', 1212),
(5, 'Healthclub Sportplatz', 'sportplatz@fit.at', '90708055', 2222);

-- --------------------------------------------------------

--
-- Table structure for table `geraet`
--

CREATE TABLE `geraet` (
  `gnr` int(10) UNSIGNED NOT NULL,
  `beschreibung` varchar(100) COLLATE utf8_german2_ci DEFAULT NULL,
  `name` varchar(50) COLLATE utf8_german2_ci NOT NULL,
  `anschaffdat` date NOT NULL,
  `anschaffpreis` decimal(10,2) NOT NULL,
  `garantie` int(10) UNSIGNED DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_german2_ci;

--
-- Dumping data for table `geraet`
--

INSERT INTO `geraet` (`gnr`, `beschreibung`, `name`, `anschaffdat`, `anschaffpreis`, `garantie`) VALUES
(1, 'Laufband fuer Einsteiger', 'Laufband run now 1', '2005-10-11', '327.90', 24),
(2, 'Laufband fuer Einsteiger', 'Laufband run now 2', '2005-10-11', '327.90', 24),
(3, 'Laufband fuer Einsteiger', 'Laufband run now 3', '2005-10-11', '327.90', 24),
(4, 'Laufband fuer Einsteiger', 'Laufband run now 4', '2005-10-11', '327.90', 24),
(5, 'Laufband fuer Einsteiger', 'Laufband run now 5', '2005-10-11', '327.90', 24),
(6, 'Laufband fuer Einsteiger', 'Laufband run now 6', '2006-10-11', '311.50', 24),
(7, 'Laufband fuer Einsteiger', 'Laufband run now 7', '2006-10-11', '311.50', 24),
(8, 'Laufband fuer Einsteiger', 'Laufband run now 8', '2006-10-11', '311.50', 24),
(9, 'Laufband fuer Fortgeschrittene', 'Laufband runner pro magnetic 1', '2017-10-11', '698.90', 24),
(10, 'Laufband fuer Fortgeschrittene', 'Laufband runner pro magnetic 2', '2017-10-11', '698.90', 24),
(11, 'Laufband fuer Fortgeschrittene', 'Laufband runner pro magnetic 3', '2017-10-11', '698.90', 24),
(12, 'Laufband fuer Fortgeschrittene', 'Laufband runner pro magnetic 4', '2018-10-11', '655.20', 24),
(13, 'Laufband fuer Fortgeschrittene', 'Laufband runner pro magnetic 5', '2018-10-11', '655.20', 24),
(14, 'Ergometer fuer jeden Fitnesslevel', 'Elliptical ergometer elite 1', '2017-10-11', '570.90', 24),
(15, 'Ergometer fuer jeden Fitnesslevel', 'Elliptical ergometer elite 2', '2017-10-11', '570.90', 24),
(16, 'Ergometer fuer jeden Fitnesslevel', 'Elliptical ergometer elite 3', '2017-10-11', '570.90', 24),
(17, 'Ergometer fuer jeden Fitnesslevel', 'Elliptical ergometer elite 4', '2018-10-11', '575.90', 24),
(18, 'Ergometer fuer jeden Fitnesslevel', 'Elliptical ergometer elite 5', '2018-10-11', '575.90', 24),
(19, 'ueber 45 Uebungsmoeglichkeiten, maximales Benutzergewicht: ca. 130kg', 'Multistation station profi center 1', '2015-10-11', '2435.90', 48),
(20, 'ueber 45 Uebungsmoeglichkeiten, maximales Benutzergewicht: ca. 130kg', 'Multistation station profi center 2', '2018-10-11', '4190.00', 48),
(21, 'ueber 45 Uebungsmoeglichkeiten, maximales Benutzergewicht: ca. 130kg', 'Multistation station profi center 3', '2018-10-11', '4190.00', 48),
(22, 'ueber 45 Uebungsmoeglichkeiten, maximales Benutzergewicht: ca. 130kg', 'Multistation station profi center 4', '2018-10-11', '4190.00', 48),
(23, 'ueber 45 Uebungsmoeglichkeiten, maximales Benutzergewicht: ca. 130kg', 'Multistation station profi center 5', '2018-10-11', '4190.00', 48),
(24, 'Multi-Hantelbank fuer extreme Gewichte und extreme Ansprueche', 'Multi Hantelbank', '2017-09-01', '429.90', 24),
(25, 'geeignet fuer alle 30/31mm und 50/51mm Langhantelstangen', 'Deluxe Kniebeugenstaender', '2017-06-01', '399.90', 5),
(26, 'Laufband fuer Einsteiger', 'Laufband run now 1', '2005-10-11', '327.90', 24),
(27, 'Laufband fuer Einsteiger', 'Laufband run now 2', '2005-10-11', '327.90', 24),
(28, 'Laufband fuer Einsteiger', 'Laufband run now 3', '2005-10-11', '327.90', 24),
(29, 'Laufband fuer Einsteiger', 'Laufband run now 4', '2005-10-11', '327.90', 24),
(30, 'Laufband fuer Einsteiger', 'Laufband run now 5', '2005-10-11', '327.90', 24),
(31, 'Laufband fuer Einsteiger', 'Laufband run now 6', '2006-10-11', '311.50', 24),
(32, 'Laufband fuer Einsteiger', 'Laufband run now 7', '2006-10-11', '311.50', 24),
(33, 'Laufband fuer Einsteiger', 'Laufband run now 8', '2006-10-11', '311.50', 24),
(34, 'Laufband fuer Fortgeschrittene', 'Laufband runner pro magnetic 1', '2017-10-11', '698.90', 24),
(35, 'Laufband fuer Fortgeschrittene', 'Laufband runner pro magnetic 2', '2017-10-11', '698.90', 24),
(36, 'Laufband fuer Fortgeschrittene', 'Laufband runner pro magnetic 3', '2017-10-11', '698.90', 24),
(37, 'Laufband fuer Fortgeschrittene', 'Laufband runner pro magnetic 4', '2018-10-11', '655.20', 24),
(38, 'Laufband fuer Fortgeschrittene', 'Laufband runner pro magnetic 5', '2018-10-11', '655.20', 24),
(39, 'Ergometer fuer jeden Fitnesslevel', 'Elliptical ergometer elite 1', '2017-10-11', '570.90', 24),
(40, 'Ergometer fuer jeden Fitnesslevel', 'Elliptical ergometer elite 2', '2017-10-11', '570.90', 24),
(41, 'Ergometer fuer jeden Fitnesslevel', 'Elliptical ergometer elite 3', '2017-10-11', '570.90', 24),
(42, 'Ergometer fuer jeden Fitnesslevel', 'Elliptical ergometer elite 4', '2018-10-11', '575.90', 24),
(43, 'Ergometer fuer jeden Fitnesslevel', 'Elliptical ergometer elite 5', '2018-10-11', '575.90', 24),
(44, 'ueber 45 Uebungsmoeglichkeiten, maximales Benutzergewicht: ca. 130kg', 'Multistation station profi center 1', '2015-10-11', '2435.90', 48),
(45, 'ueber 45 Uebungsmoeglichkeiten, maximales Benutzergewicht: ca. 130kg', 'Multistation station profi center 2', '2018-10-11', '4190.00', 48),
(46, 'ueber 45 Uebungsmoeglichkeiten, maximales Benutzergewicht: ca. 130kg', 'Multistation station profi center 3', '2018-10-11', '4190.00', 48),
(47, 'ueber 45 Uebungsmoeglichkeiten, maximales Benutzergewicht: ca. 130kg', 'Multistation station profi center 4', '2018-10-11', '4190.00', 48),
(48, 'ueber 45 Uebungsmoeglichkeiten, maximales Benutzergewicht: ca. 130kg', 'Multistation station profi center 5', '2018-10-11', '4190.00', 48),
(49, 'Multi-Hantelbank fuer extreme Gewichte und extreme Ansprueche', 'Multi Hantelbank', '2017-09-01', '429.90', 24),
(50, 'geeignet fuer alle 30/31mm und 50/51mm Langhantelstangen', 'Deluxe Kniebeugenstaender', '2017-06-01', '399.90', 5);

-- --------------------------------------------------------

--
-- Table structure for table `ist_im`
--

CREATE TABLE `ist_im` (
  `gnr` int(10) UNSIGNED NOT NULL,
  `rnr` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_german2_ci;

--
-- Dumping data for table `ist_im`
--

INSERT INTO `ist_im` (`gnr`, `rnr`) VALUES
(1, 12),
(14, 12),
(2, 13),
(11, 13),
(19, 13),
(24, 13),
(3, 14),
(12, 14),
(15, 14),
(4, 15),
(20, 15),
(25, 15),
(5, 16),
(16, 16),
(6, 17),
(21, 17),
(7, 18),
(17, 18),
(9, 19),
(13, 19),
(22, 19),
(8, 20),
(18, 20),
(10, 21),
(23, 21);

-- --------------------------------------------------------

--
-- Table structure for table `ist_nachfolger_von`
--

CREATE TABLE `ist_nachfolger_von` (
  `nachfolger` int(10) UNSIGNED NOT NULL,
  `vorgaenger` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_german2_ci;

--
-- Dumping data for table `ist_nachfolger_von`
--

INSERT INTO `ist_nachfolger_von` (`nachfolger`, `vorgaenger`) VALUES
(11, 1),
(12, 2),
(13, 3),
(14, 4),
(16, 7),
(17, 8),
(26, 10),
(20, 11),
(19, 12),
(21, 13),
(22, 14),
(24, 16),
(25, 17);

-- --------------------------------------------------------

--
-- Table structure for table `kunde`
--

CREATE TABLE `kunde` (
  `knr` int(10) UNSIGNED NOT NULL,
  `vname` varchar(50) COLLATE utf8_german2_ci NOT NULL,
  `nname` varchar(50) COLLATE utf8_german2_ci NOT NULL,
  `gebdat` date DEFAULT NULL,
  `adresse` varchar(100) COLLATE utf8_german2_ci DEFAULT NULL,
  `telefon` varchar(30) COLLATE utf8_german2_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_german2_ci;

--
-- Dumping data for table `kunde`
--

INSERT INTO `kunde` (`knr`, `vname`, `nname`, `gebdat`, `adresse`, `telefon`) VALUES
(1, 'Zens', 'Ur', '1971-05-03', '1010 Wien', '16098457'),
(2, 'Tom', 'Atensaft', '1982-05-03', '1020 Wien', '61403057'),
(3, 'Rainer', 'Verlust', '1993-09-03', '1030 Wien', '45139098'),
(4, 'Pit', 'Zah', '2004-10-03', '1040 Wien', '16154040'),
(5, 'Otto', 'Normal', '1995-06-05', '1050 Wien', '12340991'),
(6, 'Matt', 'Ritz', '1987-04-03', '1060 Wien', '67014932'),
(7, 'Franz', 'Ose', '1988-04-03', '1070 Wien', '17936450'),
(8, 'David', 'Off', '1990-04-03', '1080 Wien', '75130956'),
(9, 'Ali', 'Gator', '1991-04-03', '1090 Wien', '74984014'),
(10, 'Dick', 'Tator', '1993-04-03', '1100 Wien', '56901406'),
(11, 'Ernst', 'Fall', '1994-05-03', '1110 Wien', '14032821'),
(12, 'Ernst', 'Haft', '1989-06-03', '1110 Wien', '94607415'),
(13, 'Max', 'Mustermann', '1993-07-10', '1120 Wien', '61980156'),
(14, 'Inge', 'Neur', '1994-08-05', '1130 Wien', '749015490'),
(15, 'Jack', 'Pott', '1989-09-11', '1140 Wien', '69758213'),
(16, 'Klaus', 'Uhr', '1994-12-13', '1150 Wien', '47891450'),
(17, 'Lisa', 'Bonn', '1989-01-15', '1160 Wien', '14063187'),
(18, 'Mark', 'Ant', '1993-03-03', '1170 Wien', '87094831'),
(19, 'Sarah', 'Jevo', '1994-10-19', '1180 Wien', '10408096'),
(20, 'Wick', 'Inger', '1989-04-20', '1190 Wien', '69781463');

-- --------------------------------------------------------

--
-- Table structure for table `kurs`
--

CREATE TABLE `kurs` (
  `knr` int(10) UNSIGNED NOT NULL,
  `bezeichnung` varchar(100) COLLATE utf8_german2_ci NOT NULL,
  `tngebuehr` decimal(7,2) NOT NULL,
  `beginn` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `ende` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_german2_ci;

--
-- Dumping data for table `kurs`
--

INSERT INTO `kurs` (`knr`, `bezeichnung`, `tngebuehr`, `beginn`, `ende`) VALUES
(1, 'Body Drill', '17.90', '2019-05-11 08:00:00', '2019-05-11 10:00:00'),
(2, 'Pilates', '15.90', '2019-05-11 17:00:00', '2019-05-11 19:00:00'),
(3, 'Yoga', '15.90', '2019-05-12 07:30:00', '2019-05-12 09:00:00'),
(4, 'Kickbox', '16.90', '2019-05-12 12:00:00', '2019-05-12 14:00:00'),
(5, 'Fatburning', '15.90', '2019-06-12 08:00:00', '2019-06-12 10:00:00'),
(6, 'Dance', '14.90', '2019-06-11 17:00:00', '2019-06-11 19:00:00'),
(7, 'Tai Chi', '18.90', '2019-07-11 08:00:00', '2019-07-11 10:00:00'),
(8, 'Spinning', '13.90', '2019-07-11 17:00:00', '2019-07-11 19:00:00'),
(9, 'Zumba', '15.90', '2019-08-11 08:00:00', '2019-08-11 10:00:00'),
(10, 'Cardio Low Intensity', '15.90', '2019-08-11 17:00:00', '2019-08-11 19:00:00'),
(11, 'Body Drill Mittelstufe', '22.90', '2019-05-12 08:00:00', '2019-05-12 10:00:00'),
(12, 'Pilates Mittelstufe', '20.90', '2019-09-11 17:00:00', '2019-09-11 19:00:00'),
(13, 'Yoga Mittelstufe', '20.90', '2019-05-12 07:30:00', '2019-05-12 09:00:00'),
(14, 'Kickbox Mittelstufe', '21.90', '2019-05-13 12:00:00', '2019-05-13 14:00:00'),
(15, 'Fatburning Mittelstufe', '20.90', '2019-06-13 08:00:00', '2019-06-13 10:00:00'),
(16, 'Tai Chi Mittelstufe', '23.90', '2019-07-15 08:00:00', '2019-07-15 10:00:00'),
(17, 'Spinning Mittelstufe', '19.90', '2019-02-11 18:00:00', '2019-02-11 20:00:00'),
(18, 'Step Aerobic Mittelstufe', '20.90', '2019-06-18 16:00:00', '2019-06-18 18:00:00'),
(19, 'Pilates Fortgeschritten', '25.90', '2019-11-04 18:00:00', '2019-11-04 20:00:00'),
(20, 'Body Drill Fortgeschritten', '29.90', '2019-05-04 08:00:00', '2019-05-04 10:00:00'),
(21, 'Yoga Fortgeschritten', '25.90', '2019-05-16 07:30:00', '2019-05-16 09:00:00'),
(22, 'Kickbox Fortgeschritten', '27.90', '2019-05-12 12:00:00', '2019-05-12 14:00:00'),
(23, 'Fatburning Fortgeschritten', '25.90', '2019-06-14 08:00:00', '2019-06-14 10:00:00'),
(24, 'Tai Chi Fortgeschritten', '30.90', '2019-07-22 08:00:00', '2019-07-22 10:00:00'),
(25, 'Spinning Fortgeschritten', '24.90', '2019-12-11 18:00:00', '2019-12-11 20:00:00'),
(26, 'Cardio High Intensity', '25.90', '2019-09-11 17:00:00', '2019-09-11 19:00:00');

-- --------------------------------------------------------

--
-- Table structure for table `mitarbeiter`
--

CREATE TABLE `mitarbeiter` (
  `svnr` int(10) UNSIGNED NOT NULL,
  `name` varchar(20) COLLATE utf8_german2_ci NOT NULL,
  `adresse` varchar(40) COLLATE utf8_german2_ci NOT NULL,
  `telefonklappe` varchar(5) COLLATE utf8_german2_ci DEFAULT NULL,
  `eintrittsdat` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_german2_ci;

--
-- Dumping data for table `mitarbeiter`
--

INSERT INTO `mitarbeiter` (`svnr`, `name`, `adresse`, `telefonklappe`, `eintrittsdat`) VALUES
(1010, 'Peddy Kuehre', '1040 Wien', '4436', '2014-01-15'),
(1111, 'Rainer Zufall', '1100 Wien', '2412', '2008-05-03'),
(1212, 'Karla Schnikof', '1210 Wien', '1699', '1985-01-01'),
(1313, 'Chris Tall', '1010 Wien', '6389', '2017-02-11'),
(1414, 'Thea Tralisch', '1110 Wien', '5904', '1965-05-11'),
(1515, 'Manu Ell', '1040 Wien', '8613', '2009-07-20'),
(1616, 'Ann Stoss', '1050 Wien', '6848', '2017-01-01'),
(1717, 'Stan Desamt', '1090 Wien', '1063', '2006-10-11'),
(1818, 'Jean Darmerie', '1040 Wien', '3090', '2012-03-20'),
(1919, 'Ann Orak', '1210 Wien', '4907', '2006-01-01'),
(2020, 'Peter Gogik', '1200 Wien', '3771', '2010-01-01'),
(2121, 'Flo Rist', '1040 Wien', '3210', '2013-01-15'),
(2222, 'Karl Kulator', '1010 Wien', '3189', '2017-02-11'),
(2323, 'Erkan Alles', '1210 Wien', '6504', '1986-01-01'),
(3333, 'Johannes Kraut', '1110 Wien', '2205', '2005-12-31'),
(4444, 'Mina Ralwasser', '1040 Wien', '7604', '2009-07-20'),
(5555, 'Phil Harmonie', '1050 Wien', '6631', '2019-01-01'),
(6666, 'Sue Perman', '1090 Wien', '9118', '2005-10-11'),
(7777, 'Ann Zuender', '1040 Wien', '1978', '2014-03-20'),
(8888, 'Manni Fest', '1210 Wien', '1135', '2018-01-01'),
(9191, 'Chris Tall', '1210 Wien', '9807', '1986-01-01'),
(9999, 'Bill Igware', '1200 Wien', '8164', '2004-10-11');

-- --------------------------------------------------------

--
-- Table structure for table `raum`
--

CREATE TABLE `raum` (
  `rnr` int(10) UNSIGNED NOT NULL,
  `fcnr` int(10) UNSIGNED DEFAULT NULL,
  `name` varchar(50) COLLATE utf8_german2_ci DEFAULT NULL,
  `kapazitaet` int(10) UNSIGNED DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_german2_ci;

--
-- Dumping data for table `raum`
--

INSERT INTO `raum` (`rnr`, `fcnr`, `name`, `kapazitaet`) VALUES
(1, 1, 'Sauna1', 15),
(2, 1, 'Sauna2', 21),
(3, 4, 'Sauna1', 10),
(4, 4, 'Sauna2', 22),
(5, 2, 'Sauna1', 25),
(6, 2, 'Sauna2', 12),
(7, 3, 'Sauna1', 27),
(8, 3, 'Sauna2', 20),
(9, 3, 'Sauna3', 40),
(10, 5, 'Sauna1', 35),
(11, 5, 'Sauna2', 15),
(12, 1, 'Gymnastikraum1', 30),
(13, 1, 'Gymnastikraum2', 35),
(14, 4, 'Gymnastikraum1', 34),
(15, 4, 'Gymnastikraum2', 35),
(16, 2, 'Gymnastikraum1', 30),
(17, 2, 'Gymnastikraum2', 20),
(18, 3, 'Gymnastikraum1', 30),
(19, 3, 'Gymnastikraum2', 28),
(20, 5, 'Gymnastikraum1', 32),
(21, 5, 'Gymnastikraum2', 24),
(22, 1, 'Fitnessraum1', 35),
(23, 1, 'Fitnessraum2', 40),
(24, 4, 'Fitnessraum1', 30),
(25, 4, 'Fitnessraum2', 35),
(26, 2, 'Fitnessraum1', 45),
(27, 2, 'Fitnessraum2', 25),
(28, 3, 'Fitnessraum1', 30),
(29, 3, 'Fitnessraum2', 20),
(30, 5, 'Fitnessraum1', 45),
(31, 5, 'Fitnessraum2', 20),
(32, 1, 'Spa', 15),
(33, 4, 'Spa', 23),
(34, 2, 'Spa', 20),
(35, 3, 'Spa', 30),
(36, 5, 'Spa', 25);

-- --------------------------------------------------------

--
-- Table structure for table `standort`
--

CREATE TABLE `standort` (
  `nr` int(10) UNSIGNED NOT NULL,
  `strasse` varchar(100) COLLATE utf8_german2_ci NOT NULL,
  `plz` int(10) UNSIGNED DEFAULT NULL,
  `ort` varchar(30) COLLATE utf8_german2_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_german2_ci;

--
-- Dumping data for table `standort`
--

INSERT INTO `standort` (`nr`, `strasse`, `plz`, `ort`) VALUES
(1, 'Fitgasse 10', 1010, 'Wien'),
(2, 'Wegmitdemspeck-Platz 11', 1020, 'Wien'),
(3, 'Fatburningstrasse 12', 1030, 'Wien'),
(4, 'Fitnessgasse 13', 1040, 'Wien'),
(5, 'Sportplatz 99', 1050, 'Wien');

-- --------------------------------------------------------

--
-- Table structure for table `teilnehmen`
--

CREATE TABLE `teilnehmen` (
  `kundennr` int(10) UNSIGNED NOT NULL,
  `kursnr` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_german2_ci;

--
-- Dumping data for table `teilnehmen`
--

INSERT INTO `teilnehmen` (`kundennr`, `kursnr`) VALUES
(1, 1),
(4, 1),
(13, 1),
(1, 2),
(9, 2),
(2, 3),
(7, 3),
(8, 3),
(15, 3),
(10, 4),
(18, 4),
(1, 5),
(2, 5),
(3, 5),
(5, 5),
(2, 6),
(2, 7),
(12, 7),
(14, 7),
(1, 8),
(6, 8),
(17, 8),
(19, 8),
(20, 8),
(2, 9),
(1, 10),
(8, 10),
(1, 11),
(1, 12),
(7, 13),
(18, 14),
(3, 15),
(2, 16),
(19, 17),
(20, 17),
(1, 19),
(1, 20),
(7, 21),
(18, 22),
(3, 23),
(2, 24),
(19, 25),
(8, 26);

-- --------------------------------------------------------

--
-- Table structure for table `trainer`
--

CREATE TABLE `trainer` (
  `svnr` int(10) UNSIGNED NOT NULL,
  `ausbildung` varchar(40) COLLATE utf8_german2_ci DEFAULT NULL,
  `stundensatz` decimal(7,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_german2_ci;

--
-- Dumping data for table `trainer`
--

INSERT INTO `trainer` (`svnr`, `ausbildung`, `stundensatz`) VALUES
(1010, 'Dance, Powerplate', '30.00'),
(1515, 'Yoga, Cardio', '40.00'),
(1616, 'Boxtraining, Zumba', '45.00'),
(1717, 'Zumba, Yoga', '40.00'),
(1818, 'Cardio, Kickboxing, Step', '50.00'),
(1919, 'Pilates, Kickboxing', '60.00'),
(2020, 'Spinning, Cardio', '50.00'),
(2121, 'Powerplate, Kickboxing', '60.00'),
(2222, 'Pilates, Yoga, Aerobic', '60.00'),
(2323, 'Woyo, Yogalates, Kickboxing', '80.00'),
(4444, 'Krafttrainer, Cardio, Kickboxing', '80.00'),
(6666, 'Spinning, Zumba, Tai Chi', '50.00'),
(7777, 'Dance, Pilates', '40.00'),
(8888, 'Boxtraining, Kickboxing', '40.00'),
(9999, 'Spinning, Zumba', '50.00');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `abhaltung`
--
ALTER TABLE `abhaltung`
  ADD PRIMARY KEY (`trainer`,`kursnr`,`raumrnr`),
  ADD KEY `kursnr` (`kursnr`),
  ADD KEY `raumrnr` (`raumrnr`);

--
-- Indexes for table `abonnement`
--
ALTER TABLE `abonnement`
  ADD PRIMARY KEY (`knr`);

--
-- Indexes for table `administration`
--
ALTER TABLE `administration`
  ADD PRIMARY KEY (`svnr`);

--
-- Indexes for table `arbeitet`
--
ALTER TABLE `arbeitet`
  ADD PRIMARY KEY (`svnr`,`fcnr`),
  ADD KEY `fcnr` (`fcnr`);

--
-- Indexes for table `einzeltraining`
--
ALTER TABLE `einzeltraining`
  ADD PRIMARY KEY (`trainer`,`kundennr`),
  ADD KEY `kundennr` (`kundennr`);

--
-- Indexes for table `fitnesscenter`
--
ALTER TABLE `fitnesscenter`
  ADD PRIMARY KEY (`nr`),
  ADD KEY `leiter` (`leiter`);

--
-- Indexes for table `geraet`
--
ALTER TABLE `geraet`
  ADD PRIMARY KEY (`gnr`);

--
-- Indexes for table `ist_im`
--
ALTER TABLE `ist_im`
  ADD PRIMARY KEY (`gnr`),
  ADD KEY `rnr` (`rnr`);

--
-- Indexes for table `ist_nachfolger_von`
--
ALTER TABLE `ist_nachfolger_von`
  ADD PRIMARY KEY (`nachfolger`,`vorgaenger`),
  ADD KEY `vorgaenger` (`vorgaenger`);

--
-- Indexes for table `kunde`
--
ALTER TABLE `kunde`
  ADD PRIMARY KEY (`knr`);

--
-- Indexes for table `kurs`
--
ALTER TABLE `kurs`
  ADD PRIMARY KEY (`knr`);

--
-- Indexes for table `mitarbeiter`
--
ALTER TABLE `mitarbeiter`
  ADD PRIMARY KEY (`svnr`);

--
-- Indexes for table `raum`
--
ALTER TABLE `raum`
  ADD PRIMARY KEY (`rnr`),
  ADD KEY `fcnr` (`fcnr`);

--
-- Indexes for table `standort`
--
ALTER TABLE `standort`
  ADD PRIMARY KEY (`nr`);

--
-- Indexes for table `teilnehmen`
--
ALTER TABLE `teilnehmen`
  ADD PRIMARY KEY (`kundennr`,`kursnr`),
  ADD KEY `kursnr` (`kursnr`);

--
-- Indexes for table `trainer`
--
ALTER TABLE `trainer`
  ADD PRIMARY KEY (`svnr`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `geraet`
--
ALTER TABLE `geraet`
  MODIFY `gnr` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=51;
--
-- AUTO_INCREMENT for table `kunde`
--
ALTER TABLE `kunde`
  MODIFY `knr` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;
--
-- AUTO_INCREMENT for table `kurs`
--
ALTER TABLE `kurs`
  MODIFY `knr` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=27;
--
-- AUTO_INCREMENT for table `raum`
--
ALTER TABLE `raum`
  MODIFY `rnr` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=37;
--
-- AUTO_INCREMENT for table `standort`
--
ALTER TABLE `standort`
  MODIFY `nr` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `abhaltung`
--
ALTER TABLE `abhaltung`
  ADD CONSTRAINT `abhaltung_ibfk_1` FOREIGN KEY (`trainer`) REFERENCES `trainer` (`svnr`),
  ADD CONSTRAINT `abhaltung_ibfk_2` FOREIGN KEY (`kursnr`) REFERENCES `kurs` (`knr`),
  ADD CONSTRAINT `abhaltung_ibfk_3` FOREIGN KEY (`raumrnr`) REFERENCES `raum` (`rnr`);

--
-- Constraints for table `abonnement`
--
ALTER TABLE `abonnement`
  ADD CONSTRAINT `abonnement_ibfk_1` FOREIGN KEY (`knr`) REFERENCES `kunde` (`knr`);

--
-- Constraints for table `administration`
--
ALTER TABLE `administration`
  ADD CONSTRAINT `administration_ibfk_1` FOREIGN KEY (`svnr`) REFERENCES `mitarbeiter` (`svnr`);

--
-- Constraints for table `arbeitet`
--
ALTER TABLE `arbeitet`
  ADD CONSTRAINT `arbeitet_ibfk_1` FOREIGN KEY (`svnr`) REFERENCES `mitarbeiter` (`svnr`),
  ADD CONSTRAINT `arbeitet_ibfk_2` FOREIGN KEY (`fcnr`) REFERENCES `fitnesscenter` (`nr`);

--
-- Constraints for table `einzeltraining`
--
ALTER TABLE `einzeltraining`
  ADD CONSTRAINT `einzeltraining_ibfk_1` FOREIGN KEY (`trainer`) REFERENCES `trainer` (`svnr`),
  ADD CONSTRAINT `einzeltraining_ibfk_2` FOREIGN KEY (`kundennr`) REFERENCES `kunde` (`knr`);

--
-- Constraints for table `fitnesscenter`
--
ALTER TABLE `fitnesscenter`
  ADD CONSTRAINT `fitnesscenter_ibfk_1` FOREIGN KEY (`nr`) REFERENCES `standort` (`nr`),
  ADD CONSTRAINT `fitnesscenter_ibfk_2` FOREIGN KEY (`leiter`) REFERENCES `mitarbeiter` (`svnr`);

--
-- Constraints for table `ist_im`
--
ALTER TABLE `ist_im`
  ADD CONSTRAINT `ist_im_ibfk_1` FOREIGN KEY (`gnr`) REFERENCES `geraet` (`gnr`),
  ADD CONSTRAINT `ist_im_ibfk_2` FOREIGN KEY (`rnr`) REFERENCES `raum` (`rnr`);

--
-- Constraints for table `ist_nachfolger_von`
--
ALTER TABLE `ist_nachfolger_von`
  ADD CONSTRAINT `ist_nachfolger_von_ibfk_1` FOREIGN KEY (`nachfolger`) REFERENCES `kurs` (`knr`),
  ADD CONSTRAINT `ist_nachfolger_von_ibfk_2` FOREIGN KEY (`vorgaenger`) REFERENCES `kurs` (`knr`);

--
-- Constraints for table `raum`
--
ALTER TABLE `raum`
  ADD CONSTRAINT `raum_ibfk_1` FOREIGN KEY (`fcnr`) REFERENCES `fitnesscenter` (`nr`);

--
-- Constraints for table `teilnehmen`
--
ALTER TABLE `teilnehmen`
  ADD CONSTRAINT `teilnehmen_ibfk_1` FOREIGN KEY (`kundennr`) REFERENCES `kunde` (`knr`),
  ADD CONSTRAINT `teilnehmen_ibfk_2` FOREIGN KEY (`kursnr`) REFERENCES `kurs` (`knr`);

--
-- Constraints for table `trainer`
--
ALTER TABLE `trainer`
  ADD CONSTRAINT `trainer_ibfk_1` FOREIGN KEY (`svnr`) REFERENCES `mitarbeiter` (`svnr`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
