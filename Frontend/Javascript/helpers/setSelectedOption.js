// @Option expect a list of select input options
// @Id should be a value to mach with the option value

export const setSelectedOption = (options = [], id) => {
  options.forEach((opt) => {
    if (opt.value === id) {
      opt.setAttribute("selected", true);
    } else {
      opt.removeAttribute("selected");
    }
  });
};
