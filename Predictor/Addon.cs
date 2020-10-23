/*using System;
using System.Collections.Generic;
using kOS.AddOns;
using kOS.AddOns.TrajectoriesAddon;
using kOS.Safe.Encapsulation;
using kOS.Safe.Encapsulation.Suffixes;
using kOS.Safe.Exceptions;
using kOS.Safe.Utilities;
using kOS.Suffixed;
using UnityEngine;
using Predictor;

namespace kOS.Predictor
{
    [kOSAddon("PR")]
    [KOSNomenclature("PRAddon")]
    public class Addon : Suffixed.Addon
    {
        //private List<KeyValuePair<Guid, Class1>> predictors = new List<KeyValuePair<Guid, Class1>>();
        //private bool isEngaged;

        public Addon(SharedObjects shared) : base(shared)
        {
            InitializeSuffixes();
        }

        private void InitializeSuffixes()
        {
            AddSuffix("IMPACTPOS", new OneArgsSuffix<GeoCoordinates, VesselTarget>(ImpactPos));
            AddSuffix("HASIMPACT", new OneArgsSuffix<BooleanValue, VesselTarget>(HasImpact));
            //AddSuffix("ENGAGE", new OneArgsSuffix<VesselTarget>(Engage));
            //AddSuffix("DISENGAGE", new OneArgsSuffix<VesselTarget>(Disengage));
        }

        private BooleanValue HasImpact(VesselTarget vessel)
        {
            return GetImpactPosition(vessel).HasValue;
        }

        private GeoCoordinates ImpactPos(VesselTarget vessel)
        {
            //Class1 pr = new Class1(vessel.Vessel);
            //if(!isEngaged)
            //    throw new KOSException("please use engage");
            CelestialBody body = vessel.Vessel.orbit.referenceBody;
            Vector3? impactVect = GetImpactPosition(vessel);
            if (impactVect != null)
            {
                var worldImpactPos = (Vector3d)impactVect + body.position;
                var lat = body.GetLatitude(worldImpactPos);
                var lng = DegreeFix(body.GetLongitude(worldImpactPos), -180);
                return new GeoCoordinates(vessel.Shared, lat, lng);
            }
            throw new KOSException("Impact position is null");
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

  
        
        public Vector3? GetImpactPosition(VesselTarget vessel)
        {
            Class1 pr = new Class1(vessel.Vessel);
            pr.ComputeTrajectory(vessel.Vessel, DescentProfile.fetch);
            foreach (Patch patch in pr.Patches)
            {
                if (patch.ImpactPosition != null)
                    return patch.ImpactPosition;
            }
            return null;
        }

        public override BooleanValue Available()
        {
            return true;
        }
    }
}
*/