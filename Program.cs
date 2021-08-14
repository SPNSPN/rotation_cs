using System;
using System.Diagnostics;

using System.Numerics;
using Rotation;

namespace Rotation_dotnet
{
    class Program
    {
		static public readonly double EPS = 0.000001;

		static public void TestVec (Vector3 v1, Vector3 v2)
		{
			Console.WriteLine(string.Format("Do Test {0} == {1}.", v1, v2));
			Debug.Assert(Math.Abs(v1.X - v2.X) < EPS);
			Debug.Assert(Math.Abs(v1.Y - v2.Y) < EPS);
			Debug.Assert(Math.Abs(v1.Z - v2.Z) < EPS);
			Console.WriteLine("OK.");
		}

		static public void TestQuat (Quaternion q1, Quaternion q2)
		{
			Console.WriteLine(string.Format("Do Test {0} == {1}.", q1, q2));
			Debug.Assert(Math.Abs(q1.X - q2.X) < EPS);
			Debug.Assert(Math.Abs(q1.Y - q2.Y) < EPS);
			Debug.Assert(Math.Abs(q1.Z - q2.Z) < EPS);
			Debug.Assert(Math.Abs(q1.W - q2.W) < EPS);
			Console.WriteLine("OK.");
		}

        static void Main (string[] args)
        {
			var rpy = Rotation.Rotation.Identity().ToRPY();
			TestVec(rpy, new Vector3(0.0F, 0.0F, 0.0F));

			var v = new Vector3((float)Math.PI / 2.0F, 0.0F, 0.0F);
			rpy = Rotation.Rotation.FromRPY(v).ToRPY();
			TestVec(rpy, v);

			v = new Vector3((float)Math.PI / 8.0F, (float)Math.PI / 6.0F, -(float)Math.PI / 3.0F);
			rpy = Rotation.Rotation.FromRPY(v).ToRPY();
			TestVec(rpy, v);

			v = new Vector3((float)Math.PI / 8.0F, (float)Math.PI / 6.0F, -(float)Math.PI / 3.0F);
			rpy = Rotation.Rotation.FromRPY(v, "sxyz").ToRPY("rzyx");
			TestVec(rpy, new Vector3(v.Z, v.Y, v.X));

			var quat = Rotation.Rotation.Identity().ToQuat();
			TestQuat(quat, new Quaternion(0.0F, 0.0F, 0.0F, 1.0F));

			var q = new Quaternion((float)Math.Cos(Math.PI / 2.0 / 2.0)
					, (float)Math.Sin(Math.PI / 2.0 / 2.0), 0.0F, 0.0F);
			quat = Rotation.Rotation.FromQuat(q).ToQuat();
			TestQuat(quat, q);

			q = new Quaternion((float)Math.Sin(Math.PI / 2.0 / 2.0), 0.0F, 0.0F
					, (float)Math.Cos(Math.PI / 2.0 / 2.0));
			rpy = Rotation.Rotation.FromQuat(q).ToRPY();
			TestVec(rpy, new Vector3((float)Math.PI / 2.0F, 0.0F, 0.0F));

			v = new Vector3(0.0F, (float)Math.PI / 3.0F, 0.0F);
			quat = Rotation.Rotation.FromRPY(v).ToQuat();
			TestQuat(quat, new Quaternion(0.0F, (float)Math.Sin(Math.PI / 3.0 / 2.0)
						, 0.0F, (float)Math.Cos(Math.PI / 3.0 / 2.0)));

			var bvec = Rotation.Rotation.Identity().ToBaseVec();
			TestVec(bvec.Item1, Vector3.UnitX);
			TestVec(bvec.Item2, Vector3.UnitY);
			TestVec(bvec.Item3, Vector3.UnitZ);

			var bv = new Tuple<Vector3, Vector3>(Vector3.UnitX, Vector3.UnitZ);
			bvec = Rotation.Rotation.FromBaseVec(bv.Item1, bv.Item2).ToBaseVec();
			TestVec(bv.Item1, bvec.Item1);
			TestVec(bv.Item2, bvec.Item2);
        }
    }
}
