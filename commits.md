Course:
Relatie one to many catre User
Un curs are un profesor care este un user
Un user poate fi profesor la mai multe cursuri
Daca UserId din cookie este la fel ca professorUserId atunci se adauga un buton care poate edita cursul
Daca pretul redus este mai mic decat pretul normal atunci apare reducerea

CreateCourse:
Se verifica daca userul este logat
Se introduc detaliile
Data se seteaza automat in constructor
ProfessorUserId se preia din cookie

CourseEdit:
Se verifica daca userul este logat
Se verifica daca userul este profesorul care a creat cursul
Se pot edita detaliile
Se poate sterge cursul

Adaugare in header:
Link catre pagina de user
Link catre pagina de cursuri

User Index:
Link catre create course

Buton de register in pagina de login
Buton de login in pagina de register