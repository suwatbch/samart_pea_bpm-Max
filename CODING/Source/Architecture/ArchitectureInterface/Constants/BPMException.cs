using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Architecture.ArchitectureInterface.Constants
{
    public class BPMException
    {
        public static string TokenNotMatch = "มีผู้เข้าใช้ระบบด้วยเลขประจำตัวเดียวกันกับคุณจากเครื่องลูกข่ายอื่น\n\nกรุณาปิดโปรแกรมและทำการล็อกอินเข้าสู่ระบบใหม่\n\nCODE(010)";
        //public static string ConnectionFail = "ไม่สามารถติดต่อกับเครื่องแม่ข่ายได้ อาจเกิดจากระบบเครือข่ายมีปัญหา\n\nกรุณาติดต่อผู้ดูแลระบบ หรือใช้วิธีการรับชำระเงินในกรณีเครือข่ายมีปัญหาแทน\n\nCODE(020)";
        //แก้ไขข้อความ เกิดความผิดพลาดของเครือข่าย 19-08-2558 
        public static string ConnectionFail = "ไม่สามารถติดต่อกับเครื่องแม่ข่ายได้ \n\nกรุณาติดต่อผู้ดูแลระบบ หรือใช้วิธีการรับชำระเงินใน Mode Offline แทน \n\nCODE(020)";
        public static string NotFoundUser = "ไม่พบข้อมูลผู้ใช้งานที่ระบุในระบบ\n\nกรุณาปิดโปรแกรมและทำการล็อกอินเข้าสู่ระบบใหม่\n\nCODE(011)";
        public static string TokenExpired = "คุณเปิดเครื่องทิ้งไว้โดยไม่ใช้งานนานเกินกว่าเวลาที่ระบบกำหนด\n\nกรุณาปิดโปรแกรมและทำการล็อกอินเข้าสู่ระบบใหม่\n\nCODE(012)";
        public static string WrongConnection = "BPM Client เครื่องนี้ถูกกำหนดให้เชื่อมต่อไปยังแม่ข่ายสาขาเท่านั้น\n\nกรุณาติดต่อ BPM Support เพื่อกำหนดการเชื่อมต่อใหม่";
    }
}
