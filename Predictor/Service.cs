using System;
using KRPC.Service;
using KRPC.Service.Attributes;
using UnityEngine;
using KRPC.SpaceCenter.Services;
using Double = System.Double;
using System.Threading;

namespace Predictor
{
    [KRPCService(Name = "PR", GameScene = GameScene.Flight)]
    public static class Service
    {
        [KRPCProcedure]
        public static KRPC.Utils.Tuple<Double, Double> ImpactPos(KRPC.SpaceCenter.Services.Vessel vessel)
        {
            Class1 pr = new Class1(vessel.InternalVessel);
            CelestialBody body = vessel.InternalVessel.orbit.referenceBody;
            Vector3? impactVect = GetImpactPosition(pr, vessel.InternalVessel);

            /*if (vessel.Situation.Equals("Flying") || vessel.Situation.Equals(VesselSituation.SubOrbital))
            {*/
                while (impactVect != null)
                {
                    var worldImpactPos = (Vector3d)impactVect + body.position;
                    var lat = body.GetLatitude(worldImpactPos);
                    var lng = DegreeFix(body.GetLongitude(worldImpactPos), -180);
                    return new KRPC.Utils.Tuple<Double, Double>(lat, lng);
                }
                throw new Exception("No impact pos");
            /*}
            throw new Exception("Cannot calculate, the vessel is in not in flight. ("+ vessel.Situation+")");*/
        }

        public static Vector3? GetImpactPosition(Class1 pr, Vessel vessel)
        {


            pr.ComputeTrajectory(vessel, DescentProfile.fetch);
            foreach (Patch patch in pr.Patches)
            {
                if (patch.ImpactPosition != null)
                    return patch.ImpactPosition;
            }
            return null;
        }
        
        public static double DegreeFix(double inAngle, double rangeStart)
        {
            double rangeEnd = rangeStart + 360.0;
            double outAngle = inAngle;
            while (outAngle > rangeEnd)
                outAngle -= 360.0;
            while (outAngle < rangeStart)
                outAngle += 360.0;
            return outAngle;
        }
    }
}
