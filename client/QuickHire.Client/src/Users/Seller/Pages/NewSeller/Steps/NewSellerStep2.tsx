import { FormGroup } from "../../../../../Shared/Forms/FormGroup";
import { ActionButton } from "../../../../../Shared/Buttons/ActionButton/ActionButton";
import { useTooltip } from "../../../../../Shared/Tooltip/Tooltip";
import { NewAddedCertification } from "../../SellerProfile/NewAddedItems/Certification/NewAddedCertification";
import { NewAddedSkill } from "../../SellerProfile/NewAddedItems/Skill/NewAddedSkill";
import { NewAddedEducation } from "../../SellerProfile/NewAddedItems/Education/NewAddedEducation";

interface Certification {
  id: number;
  certification: string;
  issuer: string;
  date: string; 
}

interface Skill {
  id: number;
  name: string;
}

interface Education {
  id: number;
  institution: string;
  degree: string;
  endYear: string;
  major: string;
}

interface Props {
  certifications: Certification[];
  skills: Skill[];
  educations: Education[]; // 👈 New
  validationErrors: {
    Certification?: string[];
    Issuer?: string[];
    Date?: string[];
    Skill?: string[];
    Institution?: string[]; // 👈 New
    Degree?: string[];
    EndYear?: string[];
    Major?: string[];
  };

  certificationInput: string;
  issuerInput: string;
  dateInput: string;
  skillInput: string;

  institutionInput: string;
  degreeInput: string;
  endYearInput: string;
  majorInput: string;

  onChange: {
    certificationInput: (value: string) => void;
    issuerInput: (value: string) => void;
    dateInput: (value: string) => void;
    skillInput: (value: string) => void;
    institutionInput: (value: string) => void;
    degreeInput: (value: string) => void;
    endYearInput: (value: string) => void;
    majorInput: (value: string) => void;
  };

  onAddCertification: () => void;
  onRemoveCertification: (id: number) => void;
  onEditCertification: (cert: Certification) => void;

  onAddSkill: () => void;
  onRemoveSkill: (id: number) => void;
  onEditSkill: (skill: Skill) => void;

  onAddEducation: () => void;
  onRemoveEducation: (id: number) => void;
  onEditEducation: (edu: Education) => void;

  onNextStep: () => void;
}


export function ProfessionalInfoStep({
  certifications,
  skills,
  validationErrors,
  certificationInput,
  issuerInput,
  dateInput,
  institutionInput,
  degreeInput,
  endYearInput,
  majorInput,
  educations,
  onAddEducation,
  onRemoveEducation,
  onEditEducation,
  skillInput,
  onChange,
  onAddCertification,
  onRemoveCertification,
  onEditCertification,
  onAddSkill,
  onRemoveSkill,
  onEditSkill,
  onNextStep,
}: Props) {
  const [showCertificationTooltip, handleShowCertificationTooltip] = useTooltip();
  const [showIssuerTooltip, handleShowIssuerTooltip] = useTooltip();
  const [showDateTooltip, handleShowDateTooltip] = useTooltip();
  const [showSkillTooltip, handleShowSkillTooltip] = useTooltip();
  const [tooltipInstitution, showTooltipInstitution] = useTooltip();
const [tooltipDegree, showTooltipDegree] = useTooltip();
const [tooltipEndYear, showTooltipEndYear] = useTooltip();
const [tooltipMajor, showTooltipMajor] = useTooltip();

  return (
    <div className="wizard-form">
      <div className="brief-description-title">Add your professional details</div>
      <FormGroup
  id="institution"
  label="Institution"
  tooltipDescription="Enter the name of the institution."
  type="text"
  value={institutionInput}
  onChange={(e) => onChange.institutionInput(e.target.value)}
  placeholder="Enter Institution"
  ariaDescribedBy="institution-help"
  onShowTooltip={showTooltipInstitution}
  showTooltip={tooltipInstitution}
  error={validationErrors.Institution || []}
/>

<FormGroup
  id="degree"
  label="Degree"
  tooltipDescription="Enter the degree earned."
  type="text"
  value={degreeInput}
  onChange={(e) => onChange.degreeInput(e.target.value)}
  placeholder="Enter Degree"
  ariaDescribedBy="degree-help"
  onShowTooltip={showTooltipDegree}
  showTooltip={tooltipDegree}
  error={validationErrors.Degree || []}
/>

<FormGroup
  id="endYear"
  label="End Year"
  tooltipDescription="Enter the year of graduation."
  type="text"
  value={endYearInput}
  onChange={(e) => onChange.endYearInput(e.target.value)}
  placeholder="Enter End Year"
  ariaDescribedBy="endyear-help"
  onShowTooltip={showTooltipEndYear}
  showTooltip={tooltipEndYear}
  error={validationErrors.EndYear || []}
/>

<FormGroup
  id="major"
  label="Major"
  tooltipDescription="Enter your major field of study."
  type="text"
  value={majorInput}
  onChange={(e) => onChange.majorInput(e.target.value)}
  placeholder="Enter Major"
  ariaDescribedBy="major-help"
  onShowTooltip={showTooltipMajor}
  showTooltip={tooltipMajor}
  error={validationErrors.Major || []}
/>

<ActionButton
  text="Add Education"
  onClick={onAddEducation}
  className="add-new-button"
  ariaLabel="Add new education"
/>

{educations.map((edu) => (
  <NewAddedEducation
    key={edu.id}
    institution={edu.institution}
    degree={edu.degree}
    endYear={edu.endYear}
    major={edu.major}
    onEdit={() => onEditEducation(edu)}
    onRemove={() => onRemoveEducation(edu.id)}
  />
))}

      <FormGroup
        id="certification"
        label="Certification"
        tooltipDescription="Enter the name of the certification."
        type="text"
        value={certificationInput}
        onChange={(e) => onChange.certificationInput(e.target.value)}
        placeholder="Enter Certification"
        ariaDescribedBy="certification-help"
        onShowTooltip={handleShowCertificationTooltip}
        showTooltip={showCertificationTooltip}
        error={validationErrors.Certification || []}
      />
      <FormGroup
        id="issuer"
        label="Issuer"
        tooltipDescription="Enter the issuing organization."
        type="text"
        value={issuerInput}
        onChange={(e) => onChange.issuerInput(e.target.value)}
        placeholder="Enter Issuer"
        ariaDescribedBy="issuer-help"
        onShowTooltip={handleShowIssuerTooltip}
        showTooltip={showIssuerTooltip}
        error={validationErrors.Issuer || []}
      />
      <FormGroup
        id="date"
        label="Date"
        tooltipDescription="Enter the certification date."
        type="date"
        value={dateInput}
        onChange={(e) => onChange.dateInput(e.target.value)}
        placeholder="Enter Date"
        ariaDescribedBy="date-help"
        onShowTooltip={handleShowDateTooltip}
        showTooltip={showDateTooltip}
        error={validationErrors.Date || []}
      />

      <ActionButton
        text="Add Certification"
        onClick={onAddCertification}
        className="add-new-button"
        ariaLabel="Add new certification"
      />

      {certifications.map((cert) => (
        <NewAddedCertification
          key={cert.id}
          certification={cert}
          onRemove={() => onRemoveCertification(cert.id)}
          onEdit={() => onEditCertification(cert)}
        />
      ))}

      <FormGroup
        id="skill"
        label="Skill"
        tooltipDescription="Enter a specific skill you possess, such as 'Graphic Design', 'JavaScript', or 'Project Management'."
        type="text"
        value={skillInput}
        onChange={(e) => onChange.skillInput(e.target.value)}
        placeholder="Enter Skill"
        ariaDescribedBy="skill-help"
        onShowTooltip={handleShowSkillTooltip}
        showTooltip={showSkillTooltip}
        error={validationErrors.Skill || []}
      />

      <ActionButton
        text="Add Skill"
        onClick={onAddSkill}
        className="add-new-button"
        ariaLabel="Add new skill"
      />

      {skills.map((skill) => (
        <NewAddedSkill
          key={skill.id}
          skill={skill.name}
          onRemove={() => onRemoveSkill(skill.id)}
          onEdit={() => onEditSkill(skill)}
        />
      ))}

      <ActionButton
        text="Save and continue"
        onClick={onNextStep}
        className="add-new-button"
        ariaLabel="Save professional info and go to next step"
      />
    </div>
  );
}
