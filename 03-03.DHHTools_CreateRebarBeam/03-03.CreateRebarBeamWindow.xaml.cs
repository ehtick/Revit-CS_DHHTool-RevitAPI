#region import

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.ComponentModel;
using System.Windows;

#endregion

namespace DHHTools
{
    public class CreateFramingWindow
    {
        private CreateFramingViewModel _viewModel;
        private DHHConstraint _dlqConstraint = new DHHConstraint();

        public CreateFramingWindow(CreateFramingViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = viewModel;
            Icon = _dlqConstraint.IconWindow;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
            _viewModel.CreateFraming();
            //CreateFraming();
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public void CreateFraming()
        {
            XYZ point1 = _viewModel.UiDoc.Selection.PickPoint("Pick start point...");
            XYZ point2 = _viewModel.UiDoc.Selection.PickPoint("Pick end point...");
            // double elevation = SelectedLevel.ProjectElevation;
            // XYZ startPoint = new XYZ(point1.X, point1.Y, elevation);
            //XYZ endPoint = new XYZ(point2.X, point2.Y, elevation);
            Curve framingLocation = Line.CreateBound(point1, point2);

            FamilySymbol familySymbol
                = FamilySymbolUtils.GetFamilySymbolFraming(_viewModel.SelectedFamilyFraming,
                    DLQUnitUtils.MmToFeet(_viewModel.RongDam),
                    DLQUnitUtils.MmToFeet(_viewModel.CaoDam));

            using (Transaction tran = new Transaction(_viewModel.Doc))
            {
                tran.Start("Create Framing");

                FamilyInstance instance = _viewModel.Doc.Create.NewFamilyInstance(framingLocation,
                    familySymbol, _viewModel.SelectedLevel,
                    StructuralType.Beam);

                instance.get_Parameter(BuiltInParameter.STRUCTURAL_BEAM_END0_ELEVATION)
                    .Set(DLQUnitUtils.MmToFeet(_viewModel.ZOffset));

                instance.get_Parameter(BuiltInParameter.STRUCTURAL_BEAM_END1_ELEVATION)
                    .Set(DLQUnitUtils.MmToFeet(_viewModel.ZOffset));

                instance.get_Parameter(BuiltInParameter.Y_JUSTIFICATION).Set(1);
                tran.Commit();
            }
        }
        private void CreateFramingWindow_OnClosing(object sender, CancelEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
