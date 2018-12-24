using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FTN.Common;

namespace Klijent
{
    public partial class Form1 : Form
    {
        public GDA gda = new GDA();
        ModelResourcesDesc modelRD = new ModelResourcesDesc();

        public List<ModelCode> listaPropertija = new List<ModelCode>();
        public List<long> lista_getVal = new List<long>();
        public List<long> lista_getExVal = new List<long>();
        public List<long> lista_getRelVal = new List<long>();
        public List<string> listaString = new List<string>();
        public List<ModelCode> properties = new List<ModelCode>();
        public List<string> distinct = new List<string>();
        public static List<long> pomocna = new List<long>();
        public string tekst;
        public long gidTab1;
        public long gidTab3;
        public List<long> gidoviTab3 = new List<long>();
        List<ModelCode> tipoviReferenciranihKlasa = new List<ModelCode>();
        public StringBuilder sb;
        public Form1()
        {
            InitializeComponent();

            comboBox3.Items.Add("TERMINAL");
            comboBox3.Items.Add("PHASEIMPENDENCEDATA");
            comboBox3.Items.Add("PERLENGTHSEQUENCEIMPEDENCE");
            comboBox3.Items.Add("PERLENGTHPHASEIMPEDENCE");
            comboBox3.Items.Add("SERIESCOMPENSATOR");
            comboBox3.Items.Add("ACLINESEGMENT");
            comboBox3.Items.Add("DCLINESEGMENT");

            comboBox2.Items.Add("TERMINAL");
            comboBox2.Items.Add("PHASEIMPENDENCEDATA");
            comboBox2.Items.Add("PERLENGTHSEQUENCEIMPEDENCE");
            comboBox2.Items.Add("PERLENGTHPHASEIMPEDENCE");
            comboBox2.Items.Add("SERIESCOMPENSATOR");
            comboBox2.Items.Add("ACLINESEGMENT");
            comboBox2.Items.Add("DCLINESEGMENT");

            comboBox4.Items.Add("TERMINAL");
            comboBox4.Items.Add("PHASEIMPENDENCEDATA");
            comboBox4.Items.Add("PERLENGTHSEQUENCEIMPEDENCE");
            comboBox4.Items.Add("PERLENGTHPHASEIMPEDENCE");
            comboBox4.Items.Add("SERIESCOMPENSATOR");
            comboBox4.Items.Add("ACLINESEGMENT");
            comboBox4.Items.Add("DCLINESEGMENT");
        }
        

        #region tab1
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            lista_getVal.Clear();
            richTextBox1.Text = string.Empty;
            comboBox1.Items.Clear();

            switch (comboBox3.SelectedItem.ToString())
            {
                case "TERMINAL":
                    lista_getVal = gda.GetExtentValues(ModelCode.TERMINAL);
                    break;
                case "PHASEIMPENDENCEDATA":
                    lista_getVal = gda.GetExtentValues(ModelCode.PHASEIMPDTA);
                    break;
                case "PERLENGTHSEQUENCEIMPEDENCE":
                    lista_getVal = gda.GetExtentValues(ModelCode.PERLNGTSEQIMPD);
                    break;
                case "PERLENGTHPHASEIMPEDENCE":
                    lista_getVal = gda.GetExtentValues(ModelCode.PERLNGTPHSIMPD);
                    break;
                case "SERIESCOMPENSATOR":
                    lista_getVal = gda.GetExtentValues(ModelCode.SERIESCMPNSTR);
                    break;
                case "ACLINESEGMENT":
                    lista_getVal = gda.GetExtentValues(ModelCode.ACLINESGMNT);
                    break;
                case "DCLINESEGMENT":
                    lista_getVal = gda.GetExtentValues(ModelCode.DCLINESGMNT);
                    break;
            }

            foreach (var v in lista_getVal) 
            {
                comboBox1.Items.Add(v);
            }
            richTextBox3.Controls.Clear();
            comboBox1.Enabled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                long gid = (long)comboBox1.SelectedItem;
                ResourceDescription rd = gda.GetValues(gid);

                int pozicija = 0;
                foreach(var i in rd.Properties)
                {
                    CheckBox c = new CheckBox();
                    c.Tag = i.ToString();
                    c.Name = i.Id.ToString();
                    c.Text = i.Id.ToString();
                    c.AutoSize = true;
                    c.Location = new Point(5, 15+ pozicija);
                    pozicija += 20;
                    // richTextBox3 = c.Name;
                    richTextBox3.Controls.Add(c);
                }
                
                button2.Enabled = true;
            }
            else
                button2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<CheckBox> c = new List<CheckBox>();

            foreach (Control i in richTextBox3.Controls)
            {
                c.Add(i as CheckBox);
            }

            //richTextBox3.Multiline = true;        NE RADI SKROLOVANJE U TEXTBOX-U pa je povecan u dizajnu
            //richTextBox3.ScrollBars = RichTextBoxScrollBars.Both;
            //richTextBox3.WordWrap = false;
            richTextBox1.Text = string.Empty;
            if (comboBox1.SelectedItem != null)
            {
                try
                {
                    long gid = (long)comboBox1.SelectedItem;
                    ResourceDescription rd = gda.GetValues(gid);
                    for(int i = 0; i < rd.Properties.Count(); i++)
                    {
                        for(int j = 0; j < c.Count(); j++)
                        {
                            if(rd.Properties[i].Id.ToString() == c[j].Text && c[j].Checked)
                            {
                                //ispis liste referenci
                                if (rd.Properties[i].Type == PropertyType.ReferenceVector)
                                {
                                    if (rd.Properties[i].AsLongs().Count > 0)
                                    {
                                        sb = new StringBuilder(100);
                                        sb.Append("Reference Vector: ");
                                        for (int jj = 0; jj < rd.Properties[i].AsLongs().Count; jj++)
                                        {
                                            sb.Append(String.Format("{0}", rd.Properties[i].AsLongs()[jj])).Append(", ");
                                        }

                                        richTextBox1.Text += sb.ToString(0, sb.Length - 2) + "\n";
                                    }
                                    else
                                    {
                                        richTextBox1.Text += "Empty reference vector" + "\n";
                                    }
                                }
                                //ispis ostalih prpertija
                                else
                                {
                                    richTextBox1.Text += "Value:" + (rd.Properties[i].GetValue() + "\n").ToString();
                                }
                            }
                        }
                    }
                }
                catch
                {
                    richTextBox1.Text = "ERROR";
                }
            }

        }

        #endregion

        //TAB2
        #region tab2
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ispis1.Text = string.Empty;
            lista_getExVal.Clear();
            richTextBox2.Text = string.Empty;
            comboBox8.Items.Clear();
            
            switch (comboBox2.SelectedItem.ToString())
                {
                case "TERMINAL":
                    lista_getExVal = gda.GetExtentValues(ModelCode.TERMINAL);
                    break;
                case "PHASEIMPENDENCEDATA":
                    lista_getExVal = gda.GetExtentValues(ModelCode.PHASEIMPDTA);
                    break;
                case "PERLENGTHSEQUENCEIMPEDENCE":
                    lista_getExVal = gda.GetExtentValues(ModelCode.PERLNGTSEQIMPD);
                    break;
                case "PERLENGTHPHASEIMPEDENCE":
                    lista_getExVal = gda.GetExtentValues(ModelCode.PERLNGTPHSIMPD);
                    break;
                case "SERIESCOMPENSATOR":
                    lista_getExVal = gda.GetExtentValues(ModelCode.SERIESCMPNSTR);
                    break;
                case "ACLINESEGMENT":
                    lista_getExVal = gda.GetExtentValues(ModelCode.ACLINESGMNT);
                    break;
                case "DCLINESEGMENT":
                    lista_getExVal = gda.GetExtentValues(ModelCode.DCLINESGMNT);
                    break;
            }

            pomocna.Clear();

            foreach (var v in lista_getExVal) 
            {
                short type = ModelCodeHelper.ExtractTypeFromGlobalId(v);
                properties = modelRD.GetAllPropertyIds((DMSType)type);
                listaString.Clear();                
                foreach (ModelCode p in properties)
                {
                     listaString.Add(p.ToString());                   
                }
                pomocna.Add(v);
            }
            
            distinct = listaString.Distinct().ToList();
            foreach (string d in distinct)
            {
                comboBox8.Items.Add(d);
            }

            comboBox8.Enabled = true;
            button4.Enabled = true;

        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox8.SelectedItem != null)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        //klik na dugme kod Get Extended Values
        private void button1_Click(object sender, EventArgs e)
        {
            ispis1.Text = string.Empty;
            tekst = "";
            if (comboBox8.SelectedItem != null)
            {
                try
                {
                    string tip = (string)comboBox8.SelectedItem;
                    
                    ModelCode tip2 = modelRD.GetModelCodeFromModelCodeName(tip);
                    List<ResourceDescription> rd = new List<ResourceDescription>();
                    foreach (long mc in pomocna)
                    {
                        rd.Add(gda.GetValues(mc));
                     
                    }

                    foreach (ResourceDescription r in rd)
                    {
                        for (int i = 0; i < r.Properties.Count(); i++)
                        {
                            if (r.Properties[i].Id == tip2)
                            {
                                if (r.Properties[i].Type == PropertyType.ReferenceVector)
                                {
                                    if (r.Properties[i].AsLongs().Count > 0)
                                    {
                                        sb = new StringBuilder(100);
                                        for (int jj = 0; jj < r.Properties[i].AsLongs().Count; jj++)
                                        {
                                            sb.Append(String.Format("0x{0:x16}", r.Properties[i].AsLongs()[jj])).Append(", ");
                                        }

                                        ispis1.Text += sb.ToString(0, sb.Length - 2) + "\n";
                                    }
                                    else
                                    {
                                        ispis1.Text += "Empty reference vector" + "\n";
                                    }
                                }
                                else
                                {
                                    ispis1.Text += "Value:" + (r.Properties[i].GetValue() + "\n").ToString();
                                }
                                                              
                            }

                        }
                        
                    }
                    rd.Clear();
                    listaString.Clear();
                    distinct.Clear();
                }
                catch
                {
                    ispis1.Text = "ERROR";
                }
            }
            


        }

        private void button4_Click(object sender, EventArgs e)
        {
            lista_getExVal.Clear();
            ispis1.Text = string.Empty;

            try
            {
                switch (comboBox2.SelectedItem.ToString())
                {
                    case "TERMINAL":
                        lista_getExVal = gda.GetExtentValues(ModelCode.TERMINAL);
                        break;
                    case "PHASEIMPENDENCEDATA":
                        lista_getExVal = gda.GetExtentValues(ModelCode.PHASEIMPDTA);
                        break;
                    case "PERLENGTHSEQUENCEIMPEDENCE":
                        lista_getExVal = gda.GetExtentValues(ModelCode.PERLNGTSEQIMPD);
                        break;
                    case "PERLENGTHPHASEIMPEDENCE":
                        lista_getExVal = gda.GetExtentValues(ModelCode.PERLNGTPHSIMPD);
                        break;
                    case "SERIESCOMPENSATOR":
                        lista_getExVal = gda.GetExtentValues(ModelCode.SERIESCMPNSTR);
                        break;
                    case "ACLINESEGMENT":
                        lista_getExVal = gda.GetExtentValues(ModelCode.ACLINESGMNT);
                        break;
                    case "DCLINESEGMENT":
                        lista_getExVal = gda.GetExtentValues(ModelCode.DCLINESGMNT);
                        break;

                }

                foreach (long gid in lista_getExVal)
                {
                    ResourceDescription rd = gda.GetValues(gid);
                    ispis1.Text += gda.IspisUTextBox(rd);
                }

            }
            catch
            {
                ispis1.Text = "ERROR!";
            }
        }
       
        #endregion

        //TAB3
        #region tab3
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipoviReferenciranihKlasa.Clear();
            lista_getRelVal.Clear();
            richTextBox2.Text = string.Empty;
            comboBox5.Items.Clear();
            comboBox6.Items.Clear();
            comboBox7.Items.Clear();
            button3.Enabled = false;

            switch (comboBox4.SelectedItem.ToString())
            {
                case "TERMINAL":
                    lista_getRelVal = gda.GetExtentValues(ModelCode.TERMINAL);
                    break;
                case "PHASEIMPENDENCEDATA":
                    lista_getRelVal = gda.GetExtentValues(ModelCode.PHASEIMPDTA);
                    break;
                case "PERLENGTHSEQUENCEIMPEDENCE":
                    lista_getRelVal = gda.GetExtentValues(ModelCode.PERLNGTSEQIMPD);
                    break;
                case "PERLENGTHPHASEIMPEDENCE":
                    lista_getRelVal = gda.GetExtentValues(ModelCode.PERLNGTPHSIMPD);
                    break;
                case "SERIESCOMPENSATOR":
                    lista_getRelVal = gda.GetExtentValues(ModelCode.SERIESCMPNSTR);
                    break;
                case "ACLINESEGMENT":
                    lista_getRelVal = gda.GetExtentValues(ModelCode.ACLINESGMNT);
                    break;
                case "DCLINESEGMENT":
                    lista_getRelVal = gda.GetExtentValues(ModelCode.DCLINESGMNT);
                    break;
            }

            foreach (var v in lista_getRelVal)
            {
                comboBox5.Items.Add(v);
                gidoviTab3.Add(v);

            }
            comboBox5.Enabled = true;
        }

        //popunjavanje 3. comboboxa iz 3. taba
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            listaPropertija.Clear();
            button3.Enabled = false;
            richTextBox2.Text = string.Empty;
            comboBox6.Items.Clear();
            richTextBox4.Controls.Clear();
            richTextBox4.Clear();

            try
            {
                long gid = (long)comboBox5.SelectedItem;

                ModelCode mc = modelRD.GetModelCodeFromId(gid);
                ResourceDescription rd = gda.GetValues(gid);


                for (int i = 0; i < rd.Properties.Count; i++)
                {
                    switch (rd.Properties[i].Type)
                    {
                        case PropertyType.Reference:
                            listaPropertija.Add(rd.Properties[i].Id);
                            break;
                        case PropertyType.ReferenceVector:
                            listaPropertija.Add(rd.Properties[i].Id);
                            break;
                    }
                }

                foreach (var v in listaPropertija)
                {
                    comboBox6.Items.Add(v);
                }
            }
            catch
            {
                richTextBox2.Text = "ERROR!";
            }

            if (comboBox5.SelectedItem != null)
            {
                comboBox6.Enabled = true;
                tipoviReferenciranihKlasa.Clear();
            }
            else
                comboBox6.Enabled = false;

        }

        //omogucen rad dugmeta i punjenje Combobox-a 7
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox4.Controls.Clear();
            richTextBox4.Clear();
            comboBox7.Items.Clear();
            button3.Enabled = false;

            long gid = (long)comboBox5.SelectedItem;
            ModelCode propertyId = (ModelCode)comboBox6.SelectedItem;
            ModelCode type;

            ResourceDescription rd = gda.GetValues(gid);        //od ovog rd traziti 
            List<long> gids = new List<long>();

            for (int i = 0; i < rd.Properties.Count; i++)
            {
                if (rd.Properties[i].Id == propertyId)
                {
                    for (int k = 0; k < rd.Properties[i].PropertyValue.LongValues.Count; k++)
                    {
                        gids.Add(rd.Properties[i].PropertyValue.LongValues[k]);
                    }
                }
            }

            tipoviReferenciranihKlasa = new List<ModelCode>();
            
            foreach(long l in gidoviTab3)
            {
                ResourceDescription resurs = gda.GetValues(l);
                long GidResursa = l;
                for(int r = 0; r < resurs.Properties.Count(); r++)
                {
                    if((resurs.Properties[r].Type == PropertyType.Reference || 
                       resurs.Properties[r].Type == PropertyType.ReferenceVector) && 
                       resurs.Properties[r].Id.ToString() == comboBox6.SelectedItem.ToString())
                    {
                        short type1 = ModelCodeHelper.ExtractTypeFromGlobalId(resurs.Properties[r].PropertyValue.LongValue);
                        ModelCode ime = 0;
                        if(resurs.Properties[r].PropertyValue.LongValue != 0)
                        {
                            ime = modelRD.GetModelCodeFromId(resurs.Properties[r].PropertyValue.LongValue);
                        }

                        if (!tipoviReferenciranihKlasa.Contains(ime) && ime != 0)
                        {
                            tipoviReferenciranihKlasa.Add(ime);
                        }
                    } 
                }
            }

            foreach (var klase in tipoviReferenciranihKlasa)
            {
                comboBox7.Items.Add(klase);
            }
            int sve = 0;
            comboBox7.Items.Add(sve);
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            ispis1.Text = string.Empty;
            lista_getExVal.Clear();
            richTextBox2.Text = string.Empty;
            richTextBox4.Controls.Clear();
            richTextBox4.Clear();

            pomocna.Clear();

            distinct = listaString.Distinct().ToList();
            foreach (string d in distinct)
            {
                //comboBox9.Items.Add(d);
            }

            try
            {
                if((int)comboBox7.SelectedItem == 0)
                {
                    richTextBox4.Enabled = false;
                    button3.Enabled = true;
                }
                
            }
            catch
            {
                richTextBox4.Enabled = true;

                switch (comboBox7.SelectedItem.ToString())
                {
                    case "TERMINAL":
                        lista_getExVal = gda.GetExtentValues(ModelCode.TERMINAL);
                        break;
                    case "PHASEIMPEDANSEDATA":
                        lista_getExVal = gda.GetExtentValues(ModelCode.PHASEIMPDTA);
                        break;
                    case "PERLENGTHSEQUENCE":
                        lista_getExVal = gda.GetExtentValues(ModelCode.PERLNGTSEQIMPD);
                        break;
                    case "PERLENGTHPHASEIMPEDANCE":
                        lista_getExVal = gda.GetExtentValues(ModelCode.PERLNGTPHSIMPD);
                        break;
                    case "SERIESCOMPENSATOR":
                        lista_getExVal = gda.GetExtentValues(ModelCode.SERIESCMPNSTR);
                        break;
                    case "ACLINESEGMENT":
                        lista_getExVal = gda.GetExtentValues(ModelCode.ACLINESGMNT);
                        break;
                    case "DCLINESEGMENT":
                        lista_getExVal = gda.GetExtentValues(ModelCode.DCLINESGMNT);
                        break;
                }

                bool postojiReferenca = false;

                foreach (var v in lista_getExVal)
                {
                    ResourceDescription resurs = gda.GetValues(v);
                    for (int r = 0; r < resurs.Properties.Count(); r++)
                    {
                        foreach (long l in resurs.Properties[r].PropertyValue.LongValues)
                        {
                            int pozicija = 0;
                            if (l == (long)comboBox5.SelectedItem)
                            {
                                gidTab3 = resurs.Id;
                                foreach (var properti in resurs.Properties)
                                {
                                    CheckBox c = new CheckBox();
                                    c.Tag = properti.Id.ToString();
                                    c.Name = properti.Id.ToString();
                                    c.Text = properti.Id.ToString();
                                    c.AutoSize = true;
                                    c.Location = new Point(5, 15 + pozicija);
                                    pozicija += 20;
                                    richTextBox4.Controls.Add(c);
                                    postojiReferenca = true;
                                }
                            }
                        }
                    }
                }

                if (!postojiReferenca)
                {
                    richTextBox4.Text = "REFERENCA NA IZABRANI OBJEKAT NE POSTOJI";
                }

                if (comboBox7.SelectedItem != null)
                    button3.Enabled = true;
                else
                    button3.Enabled = false;
            }
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox7.SelectedItem != null)
                button3.Enabled = true;
            else
                button3.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = string.Empty;

            if (comboBox7.SelectedItem.ToString().Equals("0"))
            {
                //richTextBox2.Text = "Selektovana nula SOME!!!";

                ResourceDescription resurs = gda.GetValues((long)comboBox5.SelectedItem);
               
                for (int r = 0; r < resurs.Properties.Count(); r++)
                {
                    if ((resurs.Properties[r].Type == PropertyType.Reference ||
                       resurs.Properties[r].Type == PropertyType.ReferenceVector) &&
                       resurs.Properties[r].Id.ToString() == comboBox6.SelectedItem.ToString())
                    {
                        string ispis = "";
                        foreach (long l in resurs.Properties[r].PropertyValue.LongValues)
                        {
                            ResourceDescription referenciraniResuts = gda.GetValues(l);
                            ispis += gda.IspisUTextBox(referenciraniResuts);
                            ispis += "\n";
                        }

                        richTextBox2.Text = ispis;
                    }
                }

            }
            else if(!richTextBox4.Text.Equals("REFERENCA NA IZABRANI OBJEKAT NE POSTOJI"))
            {
                List<CheckBox> c = new List<CheckBox>();

                foreach (Control i in richTextBox4.Controls)
                {
                    c.Add(i as CheckBox);
                }

                try
                {
                    ResourceDescription rd = gda.GetValues(gidTab3);
                    for (int i = 0; i < rd.Properties.Count(); i++)
                    {
                        for (int j = 0; j < c.Count(); j++)
                        {
                            if (rd.Properties[i].Id.ToString() == c[j].Text && c[j].Checked)
                            {
                                //ispis liste referenci
                                if (rd.Properties[i].Type == PropertyType.ReferenceVector)
                                {
                                    if (rd.Properties[i].AsLongs().Count > 0)
                                    {
                                        sb = new StringBuilder(100);
                                        sb.Append("Reference Vector: ");
                                        for (int jj = 0; jj < rd.Properties[i].AsLongs().Count; jj++)
                                        {
                                            sb.Append(String.Format("{0}", rd.Properties[i].AsLongs()[jj])).Append(", ");
                                        }

                                        richTextBox2.Text += sb.ToString(0, sb.Length - 2) + "\n";
                                    }
                                    else
                                    {
                                        richTextBox2.Text += "Empty reference vector" + "\n";
                                    }
                                }
                                //ispis ostalih prpertija
                                else
                                {
                                    richTextBox2.Text += "Value:" + (rd.Properties[i].GetValue() + "\n").ToString();
                                }
                            }
                        }
                    }
                }
                catch
                {
                    richTextBox2.Text = "ERROR";
                }
            }
        }   
        #endregion
    }
}
