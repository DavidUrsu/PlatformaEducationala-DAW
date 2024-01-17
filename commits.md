Adaugat Repository + Service
Interogarile CRUD si logica LINQ se alfa in Repository
In service se afla logica

Pentru a sterge baza de date in mod corect:
- daca se sterge un user se sterg toate cursurile si postarile facute

- daca se sterge un curs se sterg toate enrollment-urile ce tin de acel curs

- daca se sterge un user se sterg toate enroolment-urile ce tin de acel user