using System.Collections.Generic;

namespace MVPStream.Models.Data
{
    public static class Especialidades
    {
        private static readonly Dictionary<short, IEspecialidad> especialidades;
        static Especialidades()
		{
            especialidades = new Dictionary<short, IEspecialidad>()
				{
					{Azure.Codigo, Azure},
					{Skype.Codigo, Skype},
                    {SQL.Codigo, SQL},
                    {DS.Codigo, DS},
                    {SPDS.Codigo, SPDS},
                    {WindowsConsumer.Codigo, WindowsConsumer},
                    {Sharepoint.Codigo, Sharepoint},
                    {EnterpriseSecurity.Codigo, EnterpriseSecurity},
                    {ASPNET.Codigo, ASPNET},
                    {Exchange.Codigo, Exchange},
                    {WindowsPlatform.Codigo, WindowsPlatform},
                    {Excel.Codigo, Excel},
                    {PowerShell.Codigo, PowerShell}
                };
		}

        public static IEnumerable<IEspecialidad> All
        {
            get { return especialidades.Values; }
        }

        public static IEspecialidad ByCode(short code)
        {
            IEspecialidad result;
            return !especialidades.TryGetValue(code, out result) ? null : result;
        }

        public static IEspecialidad Azure = new AzureEspecialidad();
        public static IEspecialidad Skype = new SkypeEspecialidad();
        public static IEspecialidad SQL = new SQLEspecialidad();
        public static IEspecialidad DS = new DirectoryServicesEspecialidad();
        public static IEspecialidad SPDS = new SPDSEspecialidad();
        public static IEspecialidad WindowsConsumer = new WindowsConsumerEspecialidad();
        public static IEspecialidad Sharepoint = new SharepointEspecialidad();
        public static IEspecialidad EnterpriseSecurity = new EnterpriseSecurityEspecialidad();
        public static IEspecialidad ASPNET = new ASPNetEspecialidad();
        public static IEspecialidad Exchange = new ExchangeEspecialidad();
        public static IEspecialidad WindowsPlatform = new WindowsPlatformEspecialidad();
        public static IEspecialidad Excel = new ExcelEspecialidad();
        public static IEspecialidad PowerShell = new PowerShellEspecialidad();
    }

    

    public interface IEspecialidad
    {
        short Codigo { get; }
        string GetNombre();
    }

    public class Especialidad
    {
        public short Codigo { get; private set; }

        public Especialidad(short codigo)
        {
            Codigo = codigo;
        }

        public string GetNombre() { return string.Empty; }
    }

    #region Especialidades
    public class AzureEspecialidad : Especialidad, IEspecialidad
    {
        public AzureEspecialidad() : base(1) { }
        public new string GetNombre()
        {
            return "Azure";
        }
    }

    public class SkypeEspecialidad : Especialidad, IEspecialidad
    {
        public SkypeEspecialidad() : base(2) { }
        public new string GetNombre()
        {
            return "Skype for business";
        }
    }

    public class SQLEspecialidad : Especialidad, IEspecialidad
    {
        public SQLEspecialidad() : base(3) { }
        public new string GetNombre()
        {
            return "SQL Server";
        }
    }

    public class DirectoryServicesEspecialidad : Especialidad, IEspecialidad
    {
        public DirectoryServicesEspecialidad() : base(4) { }
        public new string GetNombre()
        {
            return "Directory Services";
        }
    }

    public class SPDSEspecialidad : Especialidad, IEspecialidad
    {
        public SPDSEspecialidad() : base(6) { }
        public new string GetNombre()
        {
            return "Software Packaging, Deployment & Servicing";
        }
    }

    public class WindowsConsumerEspecialidad : Especialidad, IEspecialidad
    {
        public WindowsConsumerEspecialidad() : base(7) { }
        public new string GetNombre()
        {
            return "Windows Consumer Apps";
        }
    }

    public class SharepointEspecialidad : Especialidad, IEspecialidad
    {
        public SharepointEspecialidad() : base(8) { }
        public new string GetNombre()
        {
            return "Sharepoint server";
        }
    }

    public class EnterpriseSecurityEspecialidad : Especialidad, IEspecialidad
    {
        public EnterpriseSecurityEspecialidad() : base(9) { }
        public new string GetNombre()
        {
            return "Enterprise security";
        }
    }

    public class ASPNetEspecialidad : Especialidad, IEspecialidad
    {
        public ASPNetEspecialidad() : base(10) { }
        public new string GetNombre()
        {
            return "ASP.NET/IIS";
        }
    }

    public class ExchangeEspecialidad : Especialidad, IEspecialidad
    {
        public ExchangeEspecialidad() : base(11) { }
        public new string GetNombre()
        {
            return "Exchange Server";
        }
    }

    public class WindowsPlatformEspecialidad : Especialidad, IEspecialidad
    {
        public WindowsPlatformEspecialidad() : base(12) { }
        public new string GetNombre()
        {
            return "Windows Platform Development";
        }
    }

    public class ExcelEspecialidad : Especialidad, IEspecialidad
    {
        public ExcelEspecialidad() : base(13) { }
        public new string GetNombre()
        {
            return "Excel";
        }
    }

    public class PowerShellEspecialidad : Especialidad, IEspecialidad
    {
        public PowerShellEspecialidad() : base(14) { }
        public new string GetNombre()
        {
            return "PowerShell";
        }
    }
    #endregion

}
