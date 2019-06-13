Aplikacja symulująca system biblioteki.
Baza danych składa się z 5 tabel:
	- Club - klub książki; ma nazwę i członków klubu
	- Person - czytelnik, który ma wypożyczone książki oraz kluby, do których należy
	- Book - książka; ma tytuł, właściciela oraz przypisany obiekt opisujący jej szczegóły
	- BookDetails - szczegóły książki, takie jak ocena czytelników, waga i data wydania
	- BookRent - tabela pełniąca rolę pośrednika w relacji wiele do wielu między książką a czytelnikiem; przechowuje informacje o tym kto wypożycza którą książkę