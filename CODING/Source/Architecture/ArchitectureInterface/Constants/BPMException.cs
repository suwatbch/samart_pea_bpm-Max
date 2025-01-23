using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Architecture.ArchitectureInterface.Constants
{
    public class BPMException
    {
        public static string TokenNotMatch = "�ռ��������к������Ţ��Шӵ�����ǡѹ�Ѻ�س�ҡ����ͧ�١�������\n\n��سһԴ�������зӡ����͡�Թ�������к�����\n\nCODE(010)";
        //public static string ConnectionFail = "�������ö�Դ��͡Ѻ����ͧ�������� �Ҩ�Դ�ҡ�к����͢����ջѭ��\n\n��سҵԴ��ͼ������к� �������Ըա���Ѻ�����Թ㹡ó����͢����ջѭ��᷹\n\nCODE(020)";
        //��䢢�ͤ��� �Դ�����Դ��Ҵ�ͧ���͢��� 19-08-2558 
        public static string ConnectionFail = "�������ö�Դ��͡Ѻ����ͧ�������� \n\n��سҵԴ��ͼ������к� �������Ըա���Ѻ�����Թ� Mode Offline ᷹ \n\nCODE(020)";
        public static string NotFoundUser = "��辺�����ż����ҹ����к���к�\n\n��سһԴ�������зӡ����͡�Թ�������к�����\n\nCODE(011)";
        public static string TokenExpired = "�س�Դ����ͧ�������������ҹ�ҹ�Թ�������ҷ���к���˹�\n\n��سһԴ�������зӡ����͡�Թ�������к�����\n\nCODE(012)";
        public static string WrongConnection = "BPM Client ����ͧ���١��˹��������������ѧ�������Ң���ҹ��\n\n��سҵԴ��� BPM Support ���͡�˹����������������";
    }
}
