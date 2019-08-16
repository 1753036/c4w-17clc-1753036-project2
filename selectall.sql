USE university;

SELECT * FROM student;

SELECT * FROM auth;

SELECT * FROM class;

SELECT * FROM section;

SELECT * FROM course;

SELECT * FROM grade_report gr
JOIN student s ON s.id = gr.student_id;