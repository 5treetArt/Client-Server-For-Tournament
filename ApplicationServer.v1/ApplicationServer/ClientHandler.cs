using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace ApplicationServer.v1
{
    public class ClientHandler
    {
        // ����� �������
        public TcpClient clSocket;

        // ����� ������������� ������� � ��������	
        public void RunClient()
        {
            // �������� ������� ����� � ������ � �����������-��������

            StreamReader rs = new StreamReader(clSocket.GetStream());
            NetworkStream ws = clSocket.GetStream();

            // ��������� ����� �������, ������� ����������� � �������

            string returnData = rs.ReadLine();
            string userName = returnData;
            Console.WriteLine(userName + " �� �������");

            // ���� ������� � ��������
            while (true)
            {
                // ��������� ��������� �� �������	
                returnData = rs.ReadLine();

                // ���� ������ �������� �������� QUIT, ������ �������������

                if (returnData.IndexOf("QUIT") > -1)
                {
                    Console.WriteLine(userName + " is offline.");
                    break;
                }
                Console.WriteLine(userName + ": " + returnData);
                returnData += "\r\n";

                Console.Write("server:");
                string str = Console.ReadLine();

                // ������ ��������� � �������� ���������� 
                // ��������� � ���������� ����� 

                byte[] dataWrite = Encoding.UTF8.GetBytes(str + "\r\n");

                ws.Write(dataWrite, 0, dataWrite.Length);
            }
            // ��������� ����� ����� ��������� �������
            clSocket.Close();
        }
    }
}