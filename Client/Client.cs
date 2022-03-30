using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Client
{
    public partial class Client : Form
    {
        Socket proxClient = null;
        public Client()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // �ͻ��˲���IP��ַ�Ͷ˿ڣ�����˭����
            try
            {
                client.Connect(txtIP.Text, Int32.Parse(txtPort.Text));
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return;
            };
            proxClient = client;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string msg = txtSend.Text;
            byte[] data = Encoding.Default.GetBytes(msg);
            proxClient.Send(data, SocketFlags.None); // ����Ϣ���͸�������
        }
    }
}