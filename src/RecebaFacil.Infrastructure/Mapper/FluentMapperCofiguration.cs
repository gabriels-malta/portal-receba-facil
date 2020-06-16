using Dapper.FluentMap;

namespace RecebaFacil.Infrastructure.Mapper
{
    public class FluentMapperCofiguration
    {
        public void Initialize()
        {
            FluentMapper.EntityMaps.Clear();
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new ContatoMapper());
                config.AddMap(new EmpresaMapper());
                config.AddMap(new EnderecoMapper());
                config.AddMap(new ExpedienteMapper());
                config.AddMap(new GrupoMapper());
                config.AddMap(new UsuarioMapper());
            });
        }
    }
}
