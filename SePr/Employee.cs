using System;


namespace SePr
{
    class Employee
    {
        public string id { get; private set; }
        public int level { get; private set; }
        public int slave { get; private set; }
        public double comm { get; private set; }
        public Employee(string id, int level,int slave, int comm)
        {
            this.id = id;
            this.level = level;
            this.slave = slave;
            this.comm = comm;
        }
        public Employee(string id, int level, int slave)
        {
            this.id = id;
            this.level = level;
            this.slave = slave;
            this.comm = 0;
        }
        public void Show()
        {
            Console.WriteLine("Id:{0} Level:{1} Slaves:{2} Commission:{3}", id, level, slave, comm);
        }
        public void AddComm(double comm)
        {
            this.comm += comm;
        }
    }
}
