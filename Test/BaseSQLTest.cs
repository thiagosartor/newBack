using Infrastructure.DAO.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class BaseSQLTest
    {
        public const string SqlCleanDB = @"DBCC CHECKIDENT ('[TBPresenca]', RESEED, 0)
                                           DBCC CHECKIDENT ('[TBAula]', RESEED, 0)
                                           DBCC CHECKIDENT ('[TBAluno]', RESEED, 0)
                                           DBCC CHECKIDENT ('[TBTurma]', RESEED, 0)

                                           DELETE FROM TBPresenca
                                           DELETE FROM TBAula
                                           DELETE FROM TBAluno
                                           DELETE FROM TBTurma";

        public const string SqlInsertTest = @"INSERT INTO [TBTurma] (Ano) VALUES (2014)
                                              INSERT INTO [TBAluno] (Nome, Turma_Id) VALUES ('Aluno Teste', 1)
                                              INSERT INTO [TBAula] (Data, Turma_Id, ChamadaRealizada) VALUES (GETDATE(), 1 , 0)";

        public const string SqlVerificaBanco = @"";

        
        public BaseSQLTest()
        {
            new BaseEFTest();// Se o banco não existe ele cria

            RepositoryBaseADO.Update(SqlCleanDB);
            RepositoryBaseADO.Insert(SqlInsertTest);
        }
    }
}
