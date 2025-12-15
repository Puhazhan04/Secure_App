# SecureApp

ASP.NET Core MVC Anwendung zur Verwaltung von Mitarbeitern und Abwesenheiten.  
Die Anwendung nutzt **Entity Framework Core** mit **SQLite** als Datenbank.

---

## Voraussetzungen

- .NET SDK 10.0 
  
---

## Projekt starten

```bash
git clone https://github.com/Puhazhan04/Secure_App.git
cd Secure_App/SecureApp
dotnet run

Projektbeschreibung

SecureApp ist eine kleine ASP.NET Core MVC-Anwendung, mit der eine Firma:
Mitarbeiter verwalten (anlegen, bearbeiten, löschen)
Abwesenheiten (Ferien, Krankheit usw.) erfassen und auswerten kann

Features im Überblick

Mitarbeiterverwaltung

Liste aller Mitarbeiter
Neuen Mitarbeiter erfassen
Mitarbeiter bearbeiten
Mitarbeiter löschen

Abwesenheitsverwaltung

Liste aller Abwesenheiten
Filter: „Heute abwesend“
Abwesenheit erfassen, bearbeiten und löschen
Auswahl des Mitarbeiters über ein Drop-down-Menü

Datenbank

Die Anwendung verwendet SQLite
Datenbankdateien (*.db, *.db-wal, *.db-shm) sind nicht versioniert
Beim ersten Start wird automatisch eine leere Datenbank erstellt
Daten können über die Oberfläche erfasst werden

Technologien

ASP.NET Core MVC
Entity Framework Core
SQLite
Razor Views




