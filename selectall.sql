USE university;

SELECT * FROM student;

SELECT * FROM auth;

SELECT * FROM class;

SELECT * FROM section;

SELECT * FROM course;

SELECT * FROM grade_report;

SELECT * FROM grade_report WHERE section_id=1002 AND student_id='1742001'

SELECT st.* FROM section s
JOIN grade_report g ON s.id = g.section_id
JOIN student st ON g.student_id = st.id
WHERE s.class_id = '17HCB' AND s.course_id='CTT011'