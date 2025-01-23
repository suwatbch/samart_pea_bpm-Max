*** ��÷ӧҹ�ͧ Authentication service
�� web service ����˹�ҷ�� cache ������ table ta.User �ҡ database �������� object �ͧ class AuthenticationController
����դس���ѵ��� Singleton �������§ 1 instance ��ʹ��÷ӧҹ ��� lifetime �ͧ instance ��� �������ҡѺ lifetime �ͧ web service
(����ա�� restart iis ������ web.config ���Դ������ҧ instance ���� ��Т����ŷ���������������)

������ա����Ҷ֧�����Ţͧ User ��˹��� 㹤����á (�к��������繤����á������� userid ���������Ҿ�����Ѻ�͹���¡ web service ��Ҵ��
hash ������������� hash �ж������繤����á) �к��� load �����Ţͧ user ����鹨ҡ database ����������� DataTable ������Ѻ add ������
ŧ hash ���������� check ��Ң����Ţͧ user ���˹�� load ��������Ǻ�ҧ ���ͷ����Ҥ��駵��� 价���ա����Ҷ֧������ User �к�����Ң������ DataTable
������������᷹���Т͢����Ũҡ database �����ʹ���� ������ա����䢢����Ţͧ User �������䢢����ŷ������� DataTable �ç� �����
��зء� 30 �Թҷ� �к�����Ң����� User �������� ����ա������¹�ŧ update ��Ѻŧ� database

���ͧ�ҡ DataTable support thread safe ੾�С����ҹ��ҹ�� 㹡����¹������ŧ DataTable �֧��ͧ synchronize �ͧ 㹷�������͡����
synchronize ���¤���� lock �ͧ .NET �� lock ���� instance �ͧ class AuthenticationController

*** ��觷������¹�ŧ� Source Code BPM
- ��� code � file SecurityBS.cs ��� CommonBS.cs � ArchtitectureBS ������¡��ҹ Authentication service
����͵�Ǩ����� web.config �ա�� add application setting key ���� "SECURITY_GATEWAY" �������ը���ҹ DataAccess
㹢�����ǡѹ��ҵԴ��� Authentication service ����� ����ѹ��Ѻ����¡�� DataAccess �蹡ѹ
- ���� Project ���� BPMSecurityService ��ǹ����� web service ������������� file ����
AuthenticationWebService.asmx ��觨�����¡�� AuthenticationController.cs �ա�� (code ����Ӥѭ� ������� class ���)

*** ��¡�� Stored procedure ��� convert ������� web service ����
- Login
stored procedure ���� ta_get_CheckAuthen
��¹�� web service ���� IsAuthenticated

- GetToken
stored procedure ���� ta_get_AuthenToken
��¹�� web service ���� GetToken

- CheckToken
stored procedure ���� ta_get_CheckToken
��¹�� web service ���� CheckToken

- LoadUserProfile
stored procedure ���� ta_get_UserProfile
��¹�� web service ���� LoadUserProfile

- Check Double Login
stored procedure ���� ta_get_CheckDoubleLogin
��¹�� web service ���� CheckLoginDouble

- UpdateCurIPReqFlag
stored procedure ���� ta_upd_CurIPReqFlag
��¹�� web service ���� UpdateCurIPReqFlag


*** �ѭ��
- �Դ token not match ������ú�ҧ
- authentication service lifetime ��������ǹҹ�ҡ��� ���ҧ���µ�ͧ������ 1 �ѹ ���� restart �ء�ѹ�͹����դ������ǡ� ok
- logic ����ͧ double login �ѧ���١��ͧ�ء�ó�
- ��鹵͹��� login ��û�Ѻ��ا�ҡ������ check IsAuthentication ���� set token ��� �������¹�� 
check IsAuthentication ���� check double login ��͹�ҡ�������� dialog Yes/No �����Ҩ� login ��͹�������
��ҡ� Yes ���� set token ������ѭ��
1. user A login �������ͧ 1
2. user A login �������ͧ 2 ��觨��� dialog �͡��Ҩ� login ��͹��� ���ͺ No (����� token ⴹ������� ����� check Authen �����)
3. user A ��Ѻ价������ͧ 1 ���Դ token not match
- ��� authentication service �Դ restart user �ء����� login ������� dialog ��Ҥس���ѧ login ��͹�����

