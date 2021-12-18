# VTYS_PROJESI

## Kısa tanıtımı (Senaryo):
Okul bilgi sisteminde, okula ait bilgilerin saklanması ve yönetilmesi söz konusudur.
Okulda, bina, derslik, fakülte, bölüm, ders, açılan ders, öğretim üyesi, öğrenci ve derse kayıt yaptırma gibi işlemleri yönetilmek için bilgiler bir tabloya saklanacaktır.
## İş Kuralları:
#### Okul bilgi sistemindeki verilerin işlenmesi için bir yöneticiye (Admin) ihtiyaç vardır.
#### Okul, binalar ve fakültelerden oluşur.
#### Okul bilgeleri içerisinde okul id numarası, adı , iletişim bilgileri ve yeri bilgileri bulunmalıdır.
#### İletişim bilgeleri ve adres bilgileri ayrı ayrı okul tablosundan başka tabloda saklanacaktır.
•	Okula ait bilgeler başka tabloda olsa bile okul silinmeden önce onların silmesi sağlanmalı.
•	Okul olmayan bir bina olmamalı.
•	Bir bina tablosunda Id numarası, adı ve hangi okula ait id numarası bilgileri bulunmalıdır.
•	Bina dersliklerden oluşur.
•	Derslik bilgileri içerisinde id, adı ve bina id bilgileri barındırmalıdır.
•	Fakülte bölümlerden oluşur.
•	Bir fakülte mutlaka bir okula bağlı olmalıdır.
•	Fakülte birden fazla bölüme sahip olabilir.
•	Fakat bir bölüm sadece bir fakülteye aittir.
•	Bölüm olmayan bir fakültede açılan ders yok anlamına gelir.
•	Açılan ders olabilmesi için ders tablosunda derslerin bilgileri bulunmalıdır.
•	Bir ders var olması bölüm varlığına bağlıdır.
•	Bir bölümde birden fazla ders bulunabilir.
•	Açılan derslere çok sayıda öğrenci tarafından kayıt yaptırılabilir.
•	Bir öğrenci çok sayıda açılan derslere (farklı farklı) başvurabilir.
•	Kayıtlı bir öğrenci, kayıtlı olduğu dersten yalnız tek puan (not) alabilir.
•	Kayıtlı olmayan öğrenci puan alamaz.
•	Bir öğrenci tablosunda id, ad,soyad, doğum tarihi, cinsiyet, seviye, foto, iletişim bilgileri yer almalıdır.
•	Bir öğrenci, öğrenci tablosuna eklenebilmesi için 10 yaşında veya daha fazla olması gerekir. 😊 (veri tabanı tasarım kısmı değil, kodlama esnasında  yazılımcı tarafından sağlanacak bir işlem olacaktır 😊).
•	Bir öğrenci gibi bir öğretim üyesi de aynı bilgilere sahip olacaktır.
•	Bir öğretim üyesi açılan birden fazla dersi verebilir.
•	Bir açılan ders ise yalnızca bir öğretim üyesi tarafından verilir.
•	Okul, öğretim üyesi ve öğrencilerin mutlaka oldukları şehir bilgileri olmalıdır.
•	Her bir şehir bir bölgeye bağlıdır.
•	Bölgeler ise ülkeye bağlıdır.
•	Bölge olmadan şehir olamayacağı gibi ülke de olmadan bir bölge olamaz.
•	Öğrenci, öğretim üyesi, kayıtlı öğrenci, kız, erkek, sınıf, ders ve bina toplam sayılarını görebilmesi için her silme ve ekleme işlemi gerçekleştirdiğinde başka bir tabloda güncel veriler saklanacaktır.

## İlişkisel Şema (Metinsel Gösterim):
•	adminUser(userId:serial, username:varchar, password:varchar)
•	building(buildingId:int, buildingName:varchar, schoolNo: int)
•	class(classId:int, className:varchar, credit:int, roomNo:int, courseNo:int, teacherNo:int, description:tex)
•	contact(contactId:serial, telefon:varchar, email:varchar, personId:int)
•	country(countryId:int, countryName:varchar)
•	course(courseId:int, courseName:varchar, departmentNo:int)
•	department(departmentId:int, departmentName:varchar, facultyNo:int)
•	enroll(studentId:int, enrollmentId:int,classNo:int, enrollmentDate:Date, semestry:varchar, description:text)
•	faculty(facultyId:int, facultyName:varchar, schoolNo:int)
•	personelcontact(contactId:serial, telefon:varchar, email:varchar, personId:int)
•	region(regionId:int, regionName:varchar, countryId:int)
•	room(roomId:int, roomName:varchar, buildingNo:int)
•	school(schoolId:int, schoolName:varchar, addressNo:int)
•	schoolcontact(contactId:serial, telefon:int, email:varchar, schoolId:int)
•	score(studentId:int, classNo:int, studentScore:double precision, description:text)
•	student(stdId:int, firstName:varchar, lastName:varchar, dateOfBirth:Date, gender:varchar, level:varchar, photo:bytea, addressNo:int)
•	teacher(stdId:int, firstName:varchar, lastName:varchar, dateOfBirth:Date, gender:varchar, qualification:varchar, photo:bytea, addressNo:int)
•	town(townId:int, townName:varchar, regionId:int)
•	statistic(id: serial, total_student:int, female_student:int , male_student:int, total_teacher:int, female_teacher:int, male_teacher:int, student_enrolled:int, total_course:int, total_class:int, total_faculty:int, total_department:int, total_building:int, total_room:int, total_prof:int, total_dr:int, total_lec:int)
