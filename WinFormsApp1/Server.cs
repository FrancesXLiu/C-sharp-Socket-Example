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
            server.Bind(new IPEndPoint(IPAddress.Parse(txtIP.Text), Int32.Parse(txtPort.Text))); // 服务器绑定IP地址和端口，IP地址从winform的txtIP文本框中获取，端口从txtPort文本框中获取
            server.Listen(10); // 开监听
            ThreadPool.QueueUserWorkItem(new WaitCallback(AcceptSocketClient), server);

        }

        private void AcceptSocketClient(object obj)
        {
            Socket server = obj as Socket;

            while (true)
            {
                Socket client = server.Accept(); // 接受客户端连接
                clientList.Add(client);
                AppendToMainBoardText(string.Format("客户端{0}已连接", client.LocalEndPoint));

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