Adaugat:
- DbContext => PlatformaDbContext
- Baza de date
- Controller User
- View pentru Login, Register si Index User

Register View:
La inregistrare verific cu LINQ daca exista deja un user cu username-ul specificat
Verific daca exista un user cu email-ul specificat
Parola trece prin SHA256
Setez cookie-ul de autentificare

User Index:
Trimit datele userului logat prin ViewBag
Adaug un buton de logout si de schimbare a email-ului si de delete account
La schimbarea email-ului verific daca exista deja un user cu email-ul specificat
La logout si delete account sterg cookie-ul de autentificare

Login View:
Verific daca exista userul cu username-ul si parola specificate
Setez cookie-ul de autentificare