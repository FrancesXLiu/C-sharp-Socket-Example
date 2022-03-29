using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace WinFormsApp1
{
    public partial class Server : Form
    {

        List<Socket> clientList = new List<Socket> ();

        public Server()
        {
            InitializeComponent();
            // CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(new IPEndPoint(IPAddress.Parse(txtIP.Text), Int32.Parse(txtPort.Text))); // ��������IP��ַ�Ͷ˿ڣ�IP��ַ��winform��txtIP�ı����л�ȡ���˿ڴ�txtPort�ı����л�ȡ
            server.Listen(10); // ������
            ThreadPool.QueueUserWorkItem(new WaitCallback(AcceptSocketClient), server);

        }

        private void AcceptSocketClient(object obj)
        {
            Socket server = obj as Socket;

            while (true)
            {
                Socket client = server.Accept(); // ���ܿͻ�������
                clientList.Add(client);
                AppendToMainBoardText(string.Format("�ͻ���{0}������", client.LocalEndPoint));

            }

        }

        private void AppendToMainBoardText(string text)
        {
            if (txtMainBoard.InvokeRequired)
            {
                txtMainBoard.BeginInvoke(new Action<string>((s) =>
                {
                    txtMainBoard.Text = s + "\r\n" + txtMainBoard.Text;
                }), text);
            } else
            {
                txtMainBoard.Text = text + "\r\n" + txtMainBoard.Text;
            }
        }
    }
}