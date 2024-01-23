Am rescris codul in tip API pentru a-l implementa cu Angular:
1. Clasa Controller este înlocuită cu ControllerBase deoarece ControllerBase este mai potrivit pentru API controllers, pentru că nu include suport pentru view-uri.
2. View() și RedirectToAction() sunt înlocuite cu metode care returnează date sau coduri HTTP.

CourseDTO
Am creat o nouă clasă CourseDTO pentru a evita circular reference.