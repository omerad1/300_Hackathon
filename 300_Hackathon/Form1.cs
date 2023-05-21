using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _300_Hackathon
{
    public partial class omer : Form
    {
        private SemaphoreSlim empty = new SemaphoreSlim(12, 12); // Initialized with 12 empty slots
        private SemaphoreSlim full = new SemaphoreSlim(0, 12); // Initialized with 0 filled slots
        private Mutex mutex = new Mutex();
        private int producerRate;
        private int consumerRate;
        private int numberProducers;
        private int numberConsumers;
        private List<Thread> producerThreads = new List<Thread>();
        private List<Thread> consumerThreads = new List<Thread>();
        private List<PictureBox> pictureBoxes = new List<PictureBox>(); // Store the picture boxes
        private Random rand = new Random();
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        

        public omer()
        {
            InitializeComponent();

            textBox1 = fight2.GetText1();

            textBox2 = fight2.GetText2();

            textBox3 = fight2.GetText3();
            textBox2.TextChanged += TextBox2_TextChanged;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numberProducers = (int)numericUpDown1.Value;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            InitializePictureBoxes();
            InitializeThreads();

            bunifuTransition2.Show(loading_warriors1);
            await Task.Delay(6000); // Wait for 2 seconds
            bunifuTransition1.Show(fight2);
            bunifuTransition2.Hide(loading_warriors1);

            StartThreads();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            numberConsumers = (int)numericUpDown3.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            producerRate = (int)numericUpDown2.Value;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            consumerRate = (int)numericUpDown4.Value;
        }

        private void InitializePictureBoxes()
        {
            pictureBoxes.Add(pictureBox1);
            pictureBoxes.Add(pictureBox2);
            pictureBoxes.Add(pictureBox3);
            pictureBoxes.Add(pictureBox4);
            pictureBoxes.Add(pictureBox5);
            pictureBoxes.Add(pictureBox6);
            pictureBoxes.Add(pictureBox7);
            pictureBoxes.Add(pictureBox8);
            pictureBoxes.Add(pictureBox9);
            pictureBoxes.Add(pictureBox10);
            pictureBoxes.Add(pictureBox11);
            pictureBoxes.Add(pictureBox12);
        }

        private void InitializeThreads()
        {
            for (int i = 0; i < numberProducers; i++)
            {
                Thread producerThread = new Thread(ProducerThread);
                producerThreads.Add(producerThread);
            }

            for (int i = 0; i < numberConsumers; i++)
            {
                Thread consumerThread = new Thread(ConsumerThread);
                producerThreads.Add(consumerThread);
            }
        }

        private void StartThreads()
        {
            foreach (Thread producerThread in producerThreads)
            {
                producerThread.Start();
            }


        }

        private void ConsumerThread()
        {
            while (true)
            {
                full.Wait(); // Wait for a filled slot
                mutex.WaitOne(); // Acquire the mutex

                int randomPosition;
                // Consume the item
                do
                {
                    randomPosition = rand.Next(maxValue: 12); // Generate a random position in the buffer
                }
                while (!pictureBoxes[randomPosition].Visible);

                pictureBoxes[randomPosition].Invoke(new MethodInvoker(delegate { pictureBoxes[randomPosition].Hide(); }));
                mutex.ReleaseMutex(); // Release the mutex
                empty.Release(); // Signal that an empty slot is available

                Thread.Sleep(consumerRate);
            }
        }

        private void ProducerThread()
        {
            while (true)
            {
                // Produce an item

                empty.Wait(); // Wait for an empty slot
                mutex.WaitOne(); // Acquire the mutex

                int randomPosition =0 ;
                // Add the item to the buffer
                do
                {
                    randomPosition = rand.Next(maxValue: 12); // Generate a random position in the buffer
                }
                while (pictureBoxes[randomPosition].Visible);

                pictureBoxes[randomPosition].Invoke(new MethodInvoker(delegate { pictureBoxes[randomPosition].Show(); }));

                mutex.ReleaseMutex(); // Release the mutex
                full.Release(); // Signal that a slot is filled

                Thread.Sleep(producerRate);
            }
        }

        private void fight2_Load(object sender, EventArgs e)
        {



        }

        private void userControl11_Load(object sender, EventArgs e)
        {

        }

        private void loading_warriors1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            int activeThreads = 0;
            foreach (ProcessThread thread in Process.GetCurrentProcess().Threads)
            {
                if (thread.ThreadState == System.Diagnostics.ThreadState.Running)
                {
                    activeThreads++;
                }
            }


            if ((numberConsumers + numberProducers) > 0)
            {
                int x = activeThreads / (numberConsumers + numberProducers);
                textBox1.Text = x.ToString() + "%";
            }
        }
    }
}
