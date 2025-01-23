*** การทำงานของ Authentication service
เป็น web service ที่ทำหน้าที่ cache ข้อมูล table ta.User จาก database มาเก็บไว้ใน object ของ class AuthenticationController
ซึ่งมีคุณสมบัติเป็น Singleton คือมีเพียง 1 instance ตลอดการทำงาน และ lifetime ของ instance นั้น จะยาวเท่ากับ lifetime ของ web service
(ถ้ามีการ restart iis หรือแก้ web.config จะเกิดการสร้าง instance ใหม่ และข้อมูลที่เก็บไว้จะหายไปหมด)

เมื่อมีการเข้าถึงข้อมูลของ User คนหนึ่งๆ ในครั้งแรก (ระบบรู้ว่าเป็นครั้งแรกได้โดยเอา userid ที่ส่งเข้ามาพร้อมกับตอนเรียก web service ไปหาดูใน
hash ถ้าไม่มีอยู่ใน hash จะถือว่าเป็นครั้งแรก) ระบบจะ load ข้อมูลของ user คนนั้นจาก database ขึ้นมาเก็บไว้ใน DataTable พร้อมกับ add ข้อมูล
ลง hash เพื่อเอาไว้ check ว่าข้อมูลของ user คนไหนเคย load ขึ้นมาแล้วบ้าง เพื่อที่ว่าครั้งต่อๆ ไปที่มีการเข้าถึงข้อมูล User ระบบจะเอาข้อมูลใน DataTable
ที่เก็บไว้มาใช้แทนที่จะขอข้อมูลจาก database ใหม่ตลอดเวลา เมื่อมีการแก้ไขข้อมูลของ User ก็ให้แก้ไขข้อมูลที่อยู่ใน DataTable ตรงๆ ได้เลย
และทุกๆ 30 วินาที ระบบจะเอาข้อมูล User ที่เก็บไว้ และมีการเปลี่ยนแปลง update กลับลงใน database

เนื่องจาก DataTable support thread safe เฉพาะการอ่านเท่านั้น ในการเขียนข้อมูลลง DataTable จึงต้อง synchronize เอง ในที่นี้เลือกใช้การ
synchronize ด้วยคำสั่ง lock ของ .NET โดย lock ด้วย instance ของ class AuthenticationController

*** สิ่งที่เปลี่ยนแปลงใน Source Code BPM
- แก้ไข code ใน file SecurityBS.cs และ CommonBS.cs ใน ArchtitectureBS ให้เรียกใช้งาน Authentication service
เมื่อตรวจพบว่า web.config มีการ add application setting key ชื่อ "SECURITY_GATEWAY" ถ้าไม่มีจะใช้งาน DataAccess
ในขณะเดียวกันถ้าติดต่อ Authentication service ไม่ได้ ก็จะหันกลับไปเรียกใช้ DataAccess เช่นกัน
- เพิ่ม Project ชื่อ BPMSecurityService ส่วนที่เป็น web service ทั้งหมดจะอยู่ใน file ชื่อ
AuthenticationWebService.asmx ซึ่งจะไปเรียกใช้ AuthenticationController.cs อีกที (code ที่สำคัญๆ จะอยู่ใน class นี้)

*** รายการ Stored procedure ที่ convert ขึ้นมาเป็น web service แล้ว
- Login
stored procedure ชื่อ ta_get_CheckAuthen
เขียนเป็น web service ชื่อ IsAuthenticated

- GetToken
stored procedure ชื่อ ta_get_AuthenToken
เขียนเป็น web service ชื่อ GetToken

- CheckToken
stored procedure ชื่อ ta_get_CheckToken
เขียนเป็น web service ชื่อ CheckToken

- LoadUserProfile
stored procedure ชื่อ ta_get_UserProfile
เขียนเป็น web service ชื่อ LoadUserProfile

- Check Double Login
stored procedure ชื่อ ta_get_CheckDoubleLogin
เขียนเป็น web service ชื่อ CheckLoginDouble

- UpdateCurIPReqFlag
stored procedure ชื่อ ta_upd_CurIPReqFlag
เขียนเป็น web service ชื่อ UpdateCurIPReqFlag


*** ปัญหา
- เกิด token not match เมื่อไรบ้าง
- authentication service lifetime อยู่ได้ยาวนานมากไหม อย่างน้อยต้องอยู่ได้ 1 วัน แล้ว restart ทุกวันตอนไม่มีคนใช้แล้วก็ ok
- logic เรื่อง double login ยังไม่ถูกต้องทุกกรณี
- ขั้นตอนการ login ควรปรับปรุงจากเดิมที่ check IsAuthentication แล้ว set token เลย ควรเปลี่ยนเป็น 
check IsAuthentication แล้ว check double login ก่อนจากนั้นให้ขึ้น dialog Yes/No ถามว่าจะ login ซ้อนหรือไม่
ถ้ากด Yes ค่อย set token เพื่อแก้ปัญหา
1. user A login ที่เครื่อง 1
2. user A login ที่เครื่อง 2 ซึ่งจะมี dialog บอกว่าจะ login ซ้อนไหม ให้ตอบ No (แต่ว่า token โดนแก้ไปแล้ว เมื่อ check Authen สำเร็จ)
3. user A กลับไปที่เครื่อง 1 จะเกิด token not match
- ถ้า authentication service เกิด restart user ทุกคนที่ login อยู่จะมี dialog ว่าคุณกำลัง login ซ้อนโผล่มา

