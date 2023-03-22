using System.Reflection;
using System.Xml.Serialization;

namespace MakeVersion_Form
{
    [Serializable]
    public class BuildInfo // I was going to reference this to the main project, but it wasn't built when I got to this.
    {
        public string Description;

        public string Version;
        public int BuildId;
        public string BuildType;
    }
    partial class Enter_Data
    { 
        private System.ComponentModel.IContainer components = null;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Events
            compile.Click += Compile_Click;

            arrowleft.Click += Arrowleft_Click;
            arrowright.Click += ArrowRight_Click;

            compile.Click += Build;
        }

        private void Build(object sender, EventArgs e)
        {
            BuildInfo BuildInfo_Instance = new BuildInfo();
            BuildInfo_Instance.BuildType = releasebuild.Checked ? "Debug" : "Release";
            BuildInfo_Instance.BuildId = int.Parse(buildid.Text);
            BuildInfo_Instance.Description = desc.Text;
            BuildInfo_Instance.Version = version.Text;

            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/" + "modinfo.xml";
            Stream stream = File.Create(path);
            XmlSerializer Xml = new XmlSerializer(typeof(BuildInfo));
            Xml.Serialize(stream, BuildInfo_Instance);
        }
        private void Compile_Click(object sender, EventArgs e) => prograss.Value = 100;

        private void Arrowleft_Click(object sender, EventArgs e) =>
    buildid.Text = (int.Parse(buildid.Text) - 1).ToString();
        private void ArrowRight_Click(object sender, EventArgs e) =>
    buildid.Text = (int.Parse(buildid.Text) + 1).ToString();

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.compile = new System.Windows.Forms.Button();
            this.modinfo = new System.Windows.Forms.Label();
            this.prograss = new System.Windows.Forms.ProgressBar();
            this.desc = new System.Windows.Forms.TextBox();
            this.releasebuild = new System.Windows.Forms.CheckBox();
            this.version = new System.Windows.Forms.TextBox();
            this.formversion = new System.Windows.Forms.Label();
            this.arrowleft = new System.Windows.Forms.Button();
            this.arrowright = new System.Windows.Forms.Button();
            this.buildid = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(636, 384);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 54);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // compile
            // 
            this.compile.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.compile.Location = new System.Drawing.Point(12, 319);
            this.compile.Name = "compile";
            this.compile.Size = new System.Drawing.Size(378, 58);
            this.compile.TabIndex = 1;
            this.compile.Text = "Build Version Data";
            this.compile.UseVisualStyleBackColor = false;
            // 
            // modinfo
            // 
            this.modinfo.Location = new System.Drawing.Point(12, 9);
            this.modinfo.Name = "modinfo";
            this.modinfo.Size = new System.Drawing.Size(378, 38);
            this.modinfo.TabIndex = 2;
            this.modinfo.Text = "Compile Mod Info";
            this.modinfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // prograss
            // 
            this.prograss.Location = new System.Drawing.Point(12, 279);
            this.prograss.Name = "prograss";
            this.prograss.Size = new System.Drawing.Size(378, 34);
            this.prograss.TabIndex = 3;
            // 
            // desc
            // 
            this.desc.Location = new System.Drawing.Point(12, 173);
            this.desc.Name = "desc";
            this.desc.Size = new System.Drawing.Size(378, 31);
            this.desc.TabIndex = 4;
            this.desc.Text = "Version Description";
            // 
            // releasebuild
            // 
            this.releasebuild.AutoSize = true;
            this.releasebuild.Location = new System.Drawing.Point(12, 227);
            this.releasebuild.Name = "releasebuild";
            this.releasebuild.Size = new System.Drawing.Size(140, 29);
            this.releasebuild.TabIndex = 5;
            this.releasebuild.Text = "Release Build";
            this.releasebuild.UseVisualStyleBackColor = true;
            // 
            // version
            // 
            this.version.Location = new System.Drawing.Point(12, 120);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(378, 31);
            this.version.TabIndex = 6;
            this.version.Text = "Version";
            // 
            // formversion
            // 
            this.formversion.AutoEllipsis = true;
            this.formversion.BackColor = System.Drawing.Color.Transparent;
            this.formversion.Location = new System.Drawing.Point(12, 47);
            this.formversion.Name = "formversion";
            this.formversion.Size = new System.Drawing.Size(378, 38);
            this.formversion.TabIndex = 7;
            this.formversion.Text = "1.0.0";
            this.formversion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // arrowleft
            // 
            this.arrowleft.Location = new System.Drawing.Point(197, 222);
            this.arrowleft.Name = "arrowleft";
            this.arrowleft.Size = new System.Drawing.Size(39, 34);
            this.arrowleft.TabIndex = 8;
            this.arrowleft.Text = "<";
            this.arrowleft.UseVisualStyleBackColor = true;
            // 
            // arrowright
            // 
            this.arrowright.Location = new System.Drawing.Point(331, 222);
            this.arrowright.Name = "arrowright";
            this.arrowright.Size = new System.Drawing.Size(39, 34);
            this.arrowright.TabIndex = 9;
            this.arrowright.Text = ">";
            this.arrowright.UseVisualStyleBackColor = true;
            // 
            // buildid
            // 
            this.buildid.Location = new System.Drawing.Point(237, 222);
            this.buildid.Name = "buildid";
            this.buildid.Size = new System.Drawing.Size(93, 34);
            this.buildid.TabIndex = 10;
            this.buildid.Text = "0";
            this.buildid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Enter_Data
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(402, 389);
            this.Controls.Add(this.buildid);
            this.Controls.Add(this.arrowright);
            this.Controls.Add(this.arrowleft);
            this.Controls.Add(this.formversion);
            this.Controls.Add(this.version);
            this.Controls.Add(this.releasebuild);
            this.Controls.Add(this.desc);
            this.Controls.Add(this.prograss);
            this.Controls.Add(this.modinfo);
            this.Controls.Add(this.compile);
            this.Controls.Add(this.button1);
            this.Name = "Enter_Data";
            this.Text = "Enter_Data";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private Button compile;
        private Label modinfo;
        private ProgressBar prograss;
        private TextBox desc;
        private CheckBox releasebuild;
        private TextBox version;
        private Label formversion;
        private Button arrowleft;
        private Button arrowright;
        private Label buildid;
    }
}