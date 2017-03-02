namespace demoCgodoi.Models
{
    public class Dispositivo : BaseDataObject
	{
        string nombre = string.Empty;
		public string Nombre
		{
			get { return nombre; }
			set { SetProperty(ref nombre, value); }
		}

		string descripcion = string.Empty;
		public string Descripcion
		{
			get { return descripcion; }
			set { SetProperty(ref descripcion, value); }
		}
        string id_usuario = string.Empty;
        public string Id_Usuario
        {
            get { return id_usuario; }
            set { SetProperty(ref id_usuario, value); }
        }

    }
}
