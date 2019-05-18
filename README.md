# Przewidywanie-Pogody
### Wprowadzenie
Projekt na zajęcia z Systemu Sztucznej Inteligencji (Sem. 4).
Program w oparciu o wykorzystane sztucznej sieci neuronowej z wykorzystaniem algorytmu wstecznej propagacji.
### Technologia
Projekt stworzony z wykorzystaniem:
- C# .NET Framework 4.6.1
- WinForms MVP pattern
- MySQL
- Connector/NET 8.0.15



### Baza danych
W katalogu DatabaseSources\Scripts znajduje się 5 plików.
ssi_database_create.sql - skrypt tworzący pustą bazę danych
ssi_database_with_data.sql - skrypt tworzący bazę danych oraz wrzucający dane znajdujące się w pliku DatabaseSources\Data\LearningAndTestingData.txt
Oraz odpowiednio dwa pliki dla programu webserv
- load_database.bat
- load_database_with_data.bat

Program DataInsert wczytuje dane z pliku DatabaseSources\Data\LearningAndTestingData.txt, łączy się z bazą danych za pomocą Connector/NET oraz wrzuca dane do bazy danych (baza musi istnieć).
Dane do servera zmienić można w klasie DBConnectio.cs w konstruktorze.
