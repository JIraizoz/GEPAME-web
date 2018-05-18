using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace GEPAMECore.LN
{
    class LN_Server
    {
        public static void Server()
        {
            Socket ss = null;
            IPEndPoint direccion;

            try
            {
                ss = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                direccion = new IPEndPoint(IPAddress.Any, 6666);

                Console.WriteLine("S --> ESCUCHANDO EN " + direccion.Address);

                ss.Bind(direccion);
                ss.Listen(10);
                Console.WriteLine("S --> SERVIDOR INICIADO");

                do
                {

                    Console.WriteLine("S --> ESPERANDO CLIENTE");
                    Socket sc = ss.Accept();
                    Console.WriteLine("S --> CLIENTE RECIBIDO");
                    new Thread(() =>
                    {
                        new ServerThread(sc);
                    }).Start();
                    //                    Thread.Sleep(2000);
                } while (true);

            }
            catch (SocketException se)
            {
                //LN_CORREO.Comunicacion(AD_LOG.LogType.Error, "Error en Socket: " + se);
            }
            catch (Exception ex)
            {
                //LN_CORREO.Comunicacion(AD_LOG.LogType.Error, "Excepción general: " + ex.Message);
            }
            finally
            {
                if (ss != null)
                    ss.Close();
            }
        }

    }

    class ServerThread
    {
        private Socket socket;
        private NetworkStream stream;
        private static X509Certificate serverCertificate = null;

        public ServerThread(Socket socket)
        {
            this.socket = socket ?? throw new ArgumentNullException(nameof(socket));
            //this.stream = new NetworkStream(this.socket, true);
            //serverCertificate = X509Certificate.CreateFromCertFile();
            this.mainThread();
        }

        private void mainThread()
        {
            //SslStream sslStream = new SslStream(this.stream, false);
            //sslStream.AuthenticateAsServer()
            bool doing = true;
            do
            {
                byte[] data = new byte[1024];

                int ndata = this.socket.Receive(data);
                string st = Encoding.Default.GetString(data, 0, ndata);

                switch (st)
                {
                    case "LOGIN":
                        this.socket.Send(Encoding.Default.GetBytes("LOGIN"));

                        data = new byte[1024];
                        ndata = this.socket.Receive(data);
                        //LOG username:password
                        st = Encoding.Default.GetString(data, 0, ndata);
                        string usr = "", pwd = "";
                        usr = st.Substring(st.IndexOf(" " + 1, st.IndexOf(":")));
                        pwd = st.Substring(st.IndexOf(":"));
                        //TODO: Comprobar en BD si usr y pwd es correcto
                        bool valid = true; //Consulta BD

                        if (valid)
                        {
                            this.socket.Send(Encoding.Default.GetBytes("OK"));
                            this.socket.Send(Encoding.Default.GetBytes(""));
                        }
                        else
                        {
                            this.socket.Send(Encoding.Default.GetBytes("FAIL"));
                            goto case "BYE";
                        }
                        break;
                    case "BYE":
                        this.socket.Send(Encoding.Default.GetBytes("BYE"));
                        socket.Disconnect(false);
                        doing = false;
                        break;
                    default:
                        break;
                }

            } while (doing);

            if (socket.Connected)
                socket.Close();

        }
    }
}

