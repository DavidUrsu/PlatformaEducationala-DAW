Enrollment:
creaza o legatura intre un user si un curs, relatie many to many

CourseController:
Enroll (id) adauga userul curent la cursul cu id-ul id
Deenroll (id) scoate userul curent de la cursul cu id-ul id

View Enrollments User:
Afiseaza cursurile la care este inscris userul curent

View Course Index:
pentru fiecare curs afiseaza optiunea de enroll si deenroll in functie de daca userul curent este inscris sau nu la cursul respectiv

Adaugat Blog cu postari

Blog View:
Afiseaza toate postarile

Index User:
Buton pentru a adauga o noua postare

Post View:
Afiseaza postarea
daca userul curent este autorul postarii, afiseaza butonul de edit si delete

TODO:
daca se sterge un user, sa se stearga si toate postarile lui si cursurile
daca este sters un curs se sterg relatiile din enrollments